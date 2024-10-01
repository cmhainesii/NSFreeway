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
    public static HighwayClass FromString(string input)
    {
        HighwayClass hwClass;
        if(input.CompareTo("0") == 0)
        {
            hwClass = HighwayClass.Interstate;
        }
        else if (input.CompareTo("1") == 0)
        {
            hwClass = HighwayClass.US_Highway;
        }
        else if (input.CompareTo("2") == 0)
        {
            hwClass = HighwayClass.State_Highway;
        }
        else if (input.CompareTo("3") == 0)
        {
            hwClass = HighwayClass.County_Road;
        }
        else
        {
            hwClass = HighwayClass.Local_Road;
        }

        return hwClass;
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
