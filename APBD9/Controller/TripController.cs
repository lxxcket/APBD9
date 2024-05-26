using APBD9.DTOs;
using APBD9.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD9.Controller;

[Route("api/")]
[ApiController]

public class TripController : ControllerBase
{
    private IGetTripsUseCase _getTripsUseCase;
    private IDeleteClientUseCase _deleteClientUseCase;
    private ICreateClientAndAssignToTheTripUseCase _createClientAndAssignToTheTripUseCase;

    public TripController(IGetTripsUseCase getTripsUseCase, IDeleteClientUseCase deleteClientUseCase, 
        ICreateClientAndAssignToTheTripUseCase clientAndAssignToTheTripUseCase)
    {
        _getTripsUseCase = getTripsUseCase;
        _deleteClientUseCase = deleteClientUseCase;
        _createClientAndAssignToTheTripUseCase = clientAndAssignToTheTripUseCase;
    }
    [HttpGet("/trips")]
    public async Task<IActionResult> GetTrips([FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
    {
       TripsPage page = await  _getTripsUseCase.GetTripsPage(pageNum, pageSize);
       return Ok(page);
    }


    [HttpDelete("/clients/{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var result = await _deleteClientUseCase.ExecuteClientDeletion(idClient);
        if (result.status)
        {
            return NoContent();
        }
        
        return BadRequest(new { ErrorMessage = result.message });
    }

    [HttpPost("/trips/{idTrip:int}/clients")]
    public async Task<IActionResult> AssignClientToTheTrip(int idTrip, ClientTripPostDTO postDto)
    {
        postDto.IdTrip = idTrip;
        var result = await _createClientAndAssignToTheTripUseCase.ExecuteClientCreationAndAssignToTheTrip(postDto);

        if (result.success)
        {
            return Created();
        }

        return BadRequest(new { ErrorMessage = result.message });
    }
}