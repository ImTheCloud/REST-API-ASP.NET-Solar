using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// Model class representing a CelestialObject
public class CelestialObject
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "The Name field is required.")]
    [MaxLength(100, ErrorMessage = "The maximum length for the name is 100 characters.")]
    public string Name { get; set; } = "";
    public int Size { get; set; }
    public int Position { get; set; }
    public double Mesh { get; set; }
    public double Obj { get; set; }
    [Url(ErrorMessage = "Please enter a valid URL.")]
    public string UrlImage { get; set; } = "";
    public DateTime PositionDateTime { get; set; }
    public string Description { get; set; } = "";
    public string BestMomentsAndSpots { get; set; } = "";
    public ICollection<Alien> Aliens { get; set; } = new List<Alien>();
    public ICollection<Satellite> Satellites { get; set; } = new List<Satellite>();

}