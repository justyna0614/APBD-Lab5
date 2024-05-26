namespace WebApp.Controllers.DTOs;

public class TripDto
{
    public string Name { get; }

    public string Description { get; }

    public DateTime DateFrom { get; }

    public DateTime DateTo { get; }

    public int MaxPeople { get; }

    public virtual ICollection<ClientDto> Clients { get; }

    public virtual ICollection<CountryDto> Countries { get; }

    public TripDto(string name, string description, DateTime dateFrom, DateTime dateTo, int maxPeople,
        ICollection<ClientDto> clients, ICollection<CountryDto> countries)
    {
        Name = name;
        Description = description;
        DateFrom = dateFrom;
        DateTo = dateTo;
        MaxPeople = maxPeople;
        Clients = clients;
        Countries = countries;
    }
}