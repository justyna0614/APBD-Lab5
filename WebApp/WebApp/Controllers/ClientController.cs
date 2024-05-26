using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Services.Exceptions;

namespace WebApp.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;
    
    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }
    
    // GET
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _clientService.Delete(id);
        }
        catch (ClientInUseException e)
        {
            return Conflict("Client cannot be deleted due to existing trips");
        }

        return NoContent();
    }
}