using IconPark.Core;

namespace IconPark;

public class DefaultConfig : TestClass
{
    public const string Size = "1em";
    public const int StrokeWidth = 4;
    public static StrokeLinecap StrokeLinecap = StrokeLinecap.Round;
    public static StrokeLinejoin StrokeLinejoin = StrokeLinejoin.Round;
    public static Theme Theme = Theme.Outline;
    public const bool Rtl = false;
    public const string Prefix = "i";
    public static Dictionary<Theme, Dictionary<string, string>> Colors = new Dictionary<Theme, Dictionary<string, string>>
    {
        {
            Theme.Outline,
            new Dictionary<string, string>(){
                { "fill", "#333" },
                { "background", "transparent" },
            }
        },
        {
            Theme.Filled,
            new Dictionary<string, string>(){
                { "fill", "#333" },
                { "background", "#FFF" },
            }
        },
        {
            Theme.TwoTone,
            new Dictionary<string, string>(){
                { "fill", "#333" },
                { "twoTone", "#2F88FF" },
            }
        },
        {
            Theme.MultiColor,
            new Dictionary<string, string>(){
                { "outStrokeColor", "#333" },
                { "outFillColor", "#2F88FF" },
                { "innerStrokeColor", "#FFF" },
                { "innerFillColor", "#43CCF8" },
            }
        }
    };
}
