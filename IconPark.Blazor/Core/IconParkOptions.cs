using OneOf;
using System.ComponentModel;

namespace IconPark;

public enum StrokeLinecap : byte
{
    [Description("butt")]
    Butt = 0,
    [Description("round")]
    Round = 1,
    [Description("square")]
    Square = 2
}

public enum StrokeLinejoin : byte
{
    [Description("miter")]
    Miter = 0,
    [Description("round")]
    Round = 1,
    [Description("bevel")]
    Bevel = 2
}

public enum Theme : byte
{
    [Description("outline")]
    Outline = 0,

    [Description("filled")]
    Filled = 1,

    [Description("two-tone")]
    TwoTone = 2,

    [Description("multi-color")]
    MultiColor = 3
}


public class DefaultConfig
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


internal class IconParkOptions: IEquatable<IconParkOptions>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Theme Theme { get; set; } = Theme.Outline;

    public OneOf<int, string> Size { get; set; } = DefaultConfig.Size;

    public int StrokeWidth { get; set; } = DefaultConfig.StrokeWidth;

    public List<string> Fill { get; set; } = new List<string>();

    internal List<string> Colors
    {
        get
        {
            var colors = new List<string>(4);

            switch (this.Theme)
            {
                case Theme.Outline:
                    {
                        var fillColort = "currentColor";
                        if (Fill.Count > 0)
                        {
                            fillColort = string.IsNullOrWhiteSpace(Fill[0])
                            ? "currentColor"
                            : Fill[0];
                        }
                        colors.Add(fillColort);
                        colors.Add("none");
                        colors.Add(fillColort);
                        colors.Add("none");

                        break;
                    }
                case Theme.Filled:
                    {
                        var fillColort = "currentColor";
                        if (Fill.Count > 0)
                        {
                            fillColort = string.IsNullOrWhiteSpace(Fill[0])
                            ? "currentColor"
                            : Fill[0];
                        }
                        colors.Add(fillColort);
                        colors.Add(fillColort);
                        colors.Add("#FFF");
                        colors.Add("#FFF");
                        break;
                    }
                case Theme.TwoTone:
                    {
                        if (Fill.Count < 2)
                        {
                            colors = new List<string> {
                                    "currentColor",
                                    DefaultConfig.Colors[Theme.TwoTone]["twoTone"] ,
                                    "currentColor",
                                    DefaultConfig.Colors[Theme.TwoTone]["twoTone"]
                                };
                        }
                        else
                        {
                            var fillColor1 = string.IsNullOrWhiteSpace(Fill[0])
                                ? "currentColor"
                                : Fill[0];
                            var fillColor2 = string.IsNullOrWhiteSpace(Fill[1])
                                ? DefaultConfig.Colors[Theme.TwoTone]["twoTone"]
                                : Fill[1];

                            colors.Add(fillColor1);
                            colors.Add(fillColor2);
                            colors.Add(fillColor1);
                            colors.Add(fillColor2);
                        }

                        break;
                    }
                case Theme.MultiColor:
                    {
                        if (Fill.Count < 3)
                        {
                            colors = new List<string> {
                                    "currentColor",
                                    DefaultConfig.Colors[Theme.MultiColor]["outFillColor"] ,
                                    DefaultConfig.Colors[Theme.MultiColor]["innerStrokeColor"],
                                    DefaultConfig.Colors[Theme.MultiColor]["innerFillColor"]
                                };
                        }
                        else
                        {
                            colors[0] = string.IsNullOrWhiteSpace(Fill[0])
                                ? "currentColor"
                                : Fill[0];
                            colors[1] = string.IsNullOrWhiteSpace(Fill[1])
                                ? DefaultConfig.Colors[Theme.MultiColor]["outFillColor"]
                                : Fill[1];
                            colors[2] = string.IsNullOrWhiteSpace(Fill[2])
                                ? DefaultConfig.Colors[Theme.MultiColor]["innerStrokeColor"]
                                : Fill[2];
                            colors[3] = string.IsNullOrWhiteSpace(Fill[3])
                                ? DefaultConfig.Colors[Theme.MultiColor]["innerFillColor"]
                                : Fill[3];
                        }

                        break;
                    }
            }
            return colors;
        }
    }

    public StrokeLinecap StrokeLinecap { get; set; } = DefaultConfig.StrokeLinecap;

    public StrokeLinejoin StrokeLinejoin { get; set; } = DefaultConfig.StrokeLinejoin;


    public override bool Equals(object? obj)
    {
        if(obj is IconParkOptions other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(IconParkOptions? other)
    {
        if(other == null) return false;
        var eq = this.Id == other.Id
            && this.Theme == other.Theme
            && this.Size.Equals(other.Size)
            && this.StrokeWidth == other.StrokeWidth
            && this.Fill == other.Fill
            && this.StrokeLinecap == other.StrokeLinecap
            && this.StrokeLinejoin == other.StrokeLinejoin;
        return eq;
    }

    public static bool operator ==(IconParkOptions? b, IconParkOptions? c)
    {
        if (ReferenceEquals(b, null))
        {
            if(ReferenceEquals(c, null))
            {
                return true;
            }
            return false;
        }
        else
        {
            return b.Equals(c);
        }
    }
    public static bool operator !=(IconParkOptions? b, IconParkOptions? c)
    {
        return !(b == c);
    }
}


