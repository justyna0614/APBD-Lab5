namespace WebApp.Controllers.DTOs;

public class ClientDto
{
    public string FirstName { get; }

    public string LastName { get; }

    public ClientDto(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}