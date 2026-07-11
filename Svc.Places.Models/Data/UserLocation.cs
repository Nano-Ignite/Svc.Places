using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nano.Data.Abstractions.Eventing.Annotations;
using Nano.Data.Abstractions.Models;
using NetTopologySuite.Geometries;

namespace Svc.Places.Models.Data;

/// <summary>
/// User Location.
/// </summary>
[Subscribe]
public class UserLocation : BaseEntity
{
    /// <summary>
    /// User Id.
    /// </summary>
    [Required]
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// User.
    /// </summary>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Coordinate.
    /// </summary>
    [Required]
    public virtual Point Coordinate { get; set; } = null!;

    /// <summary>
    /// Latitude.
    /// </summary>
    [NotMapped]
    public virtual double Latitude => this.Coordinate.Y;

    /// <summary>
    /// Longitude.
    /// </summary>
    [NotMapped]
    public virtual double Longitude => this.Coordinate.X;

    /// <summary>
    /// Altitude.
    /// </summary>
    [NotMapped]
    public virtual double Altitude => this.Coordinate.Z;
}