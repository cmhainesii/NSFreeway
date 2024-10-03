using System;
using System.ComponentModel.DataAnnotations;

namespace NSFreeway.Models;

public enum HighwayClass
{
    [Display(Name = "Interstate")]
    Interstate,
    [Display(Name ="US Highway")]
    US_Highway,
    [Display(Name = "State Highway")]
    State_Highway,
    [Display(Name = "County Road")]
    County_Road,
    [Display(Name = "Local road")]
    Local_Road
}

public class HighwayModel
{
    public string GetShortName()
    {
        string shortName = string.Empty;
        switch(RoadClass)
        {
            case HighwayClass.Interstate:
                shortName = "I-";
                break;
            case HighwayClass.State_Highway:
                shortName = $"{BeginState}-";
                break;
            case HighwayClass.US_Highway:
                shortName = "US-";
                break;
            case HighwayClass.County_Road:
                shortName = $"{BeginState}-CR-";
                break;
            case HighwayClass.Local_Road:
            default:
                shortName = "Local-";
                break;
        }

        shortName += RoadNumber;

        return shortName;
    }

    public static string ToReadableString(HighwayClass roadClass)
    {
        switch(roadClass)
        {
            case HighwayClass.Interstate:
                return "Interstate";

            case HighwayClass.US_Highway:
                return "US Highway";

            case HighwayClass.State_Highway:
                return "State Highway";

            case HighwayClass.County_Road:
                return "County Road";

            default:
            case HighwayClass.Local_Road:
                return "Local Road";
        }

    }


    
    public int Id { get; set; }
    public HighwayClass RoadClass { get; set; }
    public ushort RoadNumber { get; set; }
    public string BeginCity { get; set; } = string.Empty;
    public string BeginState { get; set; } = string.Empty;
    public string EndCity { get; set; } = string.Empty;
    public string EndState { get; set; } = string.Empty;
    public uint Length { get; set; }
    public bool IsTollRoad { get; set; }
}
