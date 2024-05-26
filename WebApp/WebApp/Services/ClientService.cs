using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Services.Exceptions;

namespace WebApp.Services;

public class ClientService
{
    private readonly MasterContext _masterContext;
    
    public ClientService(MasterContext masterContexts)
    {
        _masterContext = masterContexts;
    }
    
    public async Task<Boolean> Delete(int id)
    {
        var existAnyTrip = await _masterContext.ClientTrips.Where(ct => ct.IdClient == id).AnyAsync();
        
        if (existAnyTrip)
        {
            throw new ClientInUseException();
        }
        return await _masterContext.Clients.Where(client => client.IdClient == id)
            .ExecuteDeleteAsync() > 0;
    }
}