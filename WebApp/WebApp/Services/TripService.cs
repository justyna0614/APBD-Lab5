using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Controllers.DTOs;
using WebApp.Models;
using WebApp.Services.Exceptions;

namespace WebApp.Services;

public class TripService
{
    private readonly MasterContext _masterContext;

    public TripService(MasterContext masterContexts)
    {
        _masterContext = masterContexts;
    }

    public async Task<List<Trip>> GetAll()
    {
        return await _masterContext.Trips
            .Include(trip => trip.ClientTrips).ThenInclude(ct => ct.IdClientNavigation)
            .Include(trip => trip.IdCountries)
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync();
    }

    public async Task<Boolean> EnrollClient(int id, TripEnrolmentDto enrolment)
    {
        var tripExists = await _masterContext.Trips.Where(trip => trip.IdTrip == id).AnyAsync();
        if (!tripExists)
        {
            throw new TripNotFoundException();
        }

        var clients = await _masterContext.Clients.Where(client => client.Pesel == enrolment.Pesel).ToListAsync();
        Client client;
        if (clients.Count != 0)
        {
            client = clients[0];
            var alreadyEnrolled = await _masterContext.ClientTrips
                .Where(ct => ct.IdClient == client.IdClient && ct.IdTrip == id).AnyAsync();
            if (alreadyEnrolled)
            {
                throw new ClientAlreadyEnrolledException();
            }
        }
        else
        {
            client = await CreateClient(enrolment);
        }


        var clientTrip = CreateClientTrip(client, id, enrolment);

        return await _masterContext.SaveChangesAsync() > 0;
    }

    private async Task<Client> CreateClient(TripEnrolmentDto enrolment)
    {
        var client = new Client();
        client.FirstName = enrolment.FirstName;
        client.LastName = enrolment.LastName;
        client.Email = enrolment.Email;
        client.Telephone = enrolment.Telephone;
        client.Pesel = enrolment.Pesel;
        await _masterContext.Clients.AddAsync(client);
        await _masterContext.SaveChangesAsync();
        return client;
    }

    private ClientTrip CreateClientTrip(Client client, int tripId, TripEnrolmentDto enrolment)
    {
        var clientTrip = new ClientTrip();
        clientTrip.IdClient = client.IdClient;
        clientTrip.IdTrip = tripId;
        clientTrip.RegisteredAt = DateTime.Now;
        clientTrip.PaymentDate = enrolment.PaymentDate;
        return clientTrip;
    }
}