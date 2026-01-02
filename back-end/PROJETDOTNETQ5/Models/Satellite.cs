using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// Model class representing a Satellite
public class Satellite
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [MaxLength(100, ErrorMessage = "The maximum length for the name is 100 characters.")]
    public string Name { get; set; } = "";

    [Url(ErrorMessage = "Please enter a valid URL.")]
    public string ImageUrl { get; set; } = "";
    public string Orbit { get; set; } = "";

    public double Mass { get; set; }

    public string Description { get; set; } = "";

    public int CelestialObjectId { get; set; }

    [JsonIgnore]
    public CelestialObject? CelestialObject { get; private set; }
}