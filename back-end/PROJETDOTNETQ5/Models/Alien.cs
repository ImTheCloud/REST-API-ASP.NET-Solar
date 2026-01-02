using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// Model class representing an Alien
public class Alien
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [MaxLength(100, ErrorMessage = "The maximum length for the name is 100 characters.")]
    public string Name { get; set; } = "";
    [Url(ErrorMessage = "Please enter a valid URL.")]
    public string UrlImage { get; set; } = "";
    public string Power { get; set; } = "";
    public string Description { get; set; } = "";
    public int CelestialObjectId { get; set; }
    [JsonIgnore]
    public CelestialObject? CelestialObject { get; private set; }
}
