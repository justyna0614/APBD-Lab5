using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers.DTOs;

public class TripEnrolmentDto
{
    [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "First name has to be longer than 1 characters i shorter than 50  characters")]
    public string FirstName { get; set; }
    [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Last name has to be longer than 1 characters i shorter than 50  characters")]
    public string LastName { get; set; }
    [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Email address has to be longer than 2 characters i shorter than 50  characters")]
    public string Email { get; set; }
    [Required, StringLength(9, MinimumLength = 9, ErrorMessage = "Telephone number has to consist of 9 digits")]
    public string Telephone { get; set; }
    [Required, StringLength(11, MinimumLength = 11, ErrorMessage = "Pesel has to consist of 11 digits")]
    public string Pesel { get; set; }
    [Required]
    public int IdTrip { get; set; }
    [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Trip name has to be longer than 2 characters i shorter than 50  characters")]
    public String TripName { get; set; }
    public DateTime? PaymentDate { get; set; }
}