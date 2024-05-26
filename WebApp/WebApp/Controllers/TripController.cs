using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers.DTOs;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Exceptions;

namespace WebApp.Controllers;

[Route("api/trips")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly TripService _tripService;

    public TripController(TripService tripService)
    {
        _tripService = tripService;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var trips = await _tripService.GetAll();
        var response = new List<TripDto>();
        foreach (var trip in trips)
        {
            response.Add(toDto(trip));
        }

        return Ok(response);
    }
    
    // GET
    [HttpPost("{id:int}/clients")]
    public async Task<IActionResult> EnrolClient(int id, [FromBody] TripEnrolmentDto enrolment)
    {
        try
        {
            var result = await _tripService.EnrollClient(id, enrolment);
            if (result)
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        catch (TripNotFoundException e)
        {
            return NotFound("Trip with given id does not exist");
        }
        catch (ClientAlreadyEnrolledException e)
        {
            return BadRequest("Client has been already enrolled to the trip");
        }
    }

    private TripDto toDto(Trip trip)
    {
        var clients = new List<ClientDto>();
        foreach (var clientTrip in trip.ClientTrips)
        {
            clients.Add(toDto(clientTrip.IdClientNavigation));
        }
        var countries = new List<CountryDto>();
        foreach (var country in trip.IdCountries)
        {
            countries.Add(toDto(country));
        }

        return new TripDto(
            trip.Name,
            trip.Description,
            trip.DateFrom,
            trip.DateTo,
            trip.MaxPeople,
            clients,
            countries
        );
    }

    private CountryDto toDto(Country country)
    {
        return new CountryDto(country.Name);
    }

    private ClientDto toDto(Client client)
    {
        return new ClientDto(
            client.FirstName,
            client.LastName
        );
    }
}