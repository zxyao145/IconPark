namespace IconPark.Props;

internal class DefaultConfig 
{
    public const string Size = "1em";
    public const int StrokeWidth = 4;
    public static StrokeLinecapType StrokeLinecap = StrokeLinecapType.Round;
    public static StrokeLinejoinType StrokeLinejoin = StrokeLinejoinType.Round;
    public static ThemeType Theme = ThemeType.Outline;
    public const bool Rtl = false;
    public const string Prefix = "i";
    public static Dictionary<ThemeType, Dictionary<string, string>> Colors = new Dictionary<ThemeType, Dictionary<string, string>>
    {
        {
            ThemeType.Outline,
            new Dictionary<string, string>(){
                { "fill", "#333" },
                { "background", "transparent" },
            }
        },
        {
            ThemeType.Filled,
            new Dictionary<string, string>(){
                { "fill", "#333" },
                { "background", "#FFF" },
            }
        },
        {
            ThemeType.TwoTone,
            new Dictionary<string, string>(){
                { "fill", "#333" },
                { "twoTone", "#2F88FF" },
            }
        },
        {
            ThemeType.MultiColor,
            new Dictionary<string, string>(){
                { "outStrokeColor", "#333" },
                { "outFillColor", "#2F88FF" },
                { "innerStrokeColor", "#FFF" },
                { "innerFillColor", "#43CCF8" },
            }
        }
    };
}
