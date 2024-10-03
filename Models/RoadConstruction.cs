using System;

namespace NSFreeway.Models;

public class RoadConstruction
{
    public int Id { get; set; }
    public int RoadId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Impact { get; set; } = string.Empty;
    public string LocationDescription { get; set; } = string.Empty;

    public HighwayModel? Highway { get; set; }
}
