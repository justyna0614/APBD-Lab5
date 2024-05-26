namespace WebApp.Controllers.DTOs;

public class CountryDto
{
    public string Name { get; }

    public CountryDto(string name)
    {
        Name = name;
    }
}