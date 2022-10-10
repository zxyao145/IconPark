using OneOf;
using IconPark.Props;

namespace IconPark.Svg;

public record SvgOptions
{
    public ThemeType Theme { get; init; } = DefaultConfig.Theme;

    public OneOf<int, string> Size { get; init; } = DefaultConfig.Size;

    public int StrokeWidth { get; init; } = DefaultConfig.StrokeWidth;

    public List<string> Fill { get; init; } = new List<string>();

    public StrokeLinecapType StrokeLinecap { get; init; } = DefaultConfig.StrokeLinecap;

    public StrokeLinejoinType StrokeLinejoin { get; init; } = DefaultConfig.StrokeLinejoin;

    public string Id { get; init; } = Guid.NewGuid().ToString();

    internal List<string> Colors
    {
        get
        {
            var colors = new List<string>(4);

            switch (this.Theme)
            {
                case ThemeType.Outline:
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
                case ThemeType.Filled:
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
                case ThemeType.TwoTone:
                    {
                        if (Fill.Count < 2)
                        {
                            colors = new List<string> {
                                    "currentColor",
                                    DefaultConfig.Colors[ThemeType.TwoTone]["twoTone"] ,
                                    "currentColor",
                                    DefaultConfig.Colors[ThemeType.TwoTone]["twoTone"]
                                };
                        }
                        else
                        {
                            var fillColor1 = string.IsNullOrWhiteSpace(Fill[0])
                                ? "currentColor"
                                : Fill[0];
                            var fillColor2 = string.IsNullOrWhiteSpace(Fill[1])
                                ? DefaultConfig.Colors[ThemeType.TwoTone]["twoTone"]
                                : Fill[1];

                            colors.Add(fillColor1);
                            colors.Add(fillColor2);
                            colors.Add(fillColor1);
                            colors.Add(fillColor2);
                        }

                        break;
                    }
                case ThemeType.MultiColor:
                    {
                        if (Fill.Count < 3)
                        {
                            colors = new List<string> {
                                    "currentColor",
                                    DefaultConfig.Colors[ThemeType.MultiColor]["outFillColor"] ,
                                    DefaultConfig.Colors[ThemeType.MultiColor]["innerStrokeColor"],
                                    DefaultConfig.Colors[ThemeType.MultiColor]["innerFillColor"]
                                };
                        }
                        else
                        {
                            var c0 = string.IsNullOrWhiteSpace(Fill[0])
                                ? "currentColor"
                                : Fill[0];
                            var c1 = string.IsNullOrWhiteSpace(Fill[1])
                                ? DefaultConfig.Colors[ThemeType.MultiColor]["outFillColor"]
                                : Fill[1];
                            var c2 = string.IsNullOrWhiteSpace(Fill[2])
                                ? DefaultConfig.Colors[ThemeType.MultiColor]["innerStrokeColor"]
                                : Fill[2];
                            var c3 = string.IsNullOrWhiteSpace(Fill[3])
                                ? DefaultConfig.Colors[ThemeType.MultiColor]["innerFillColor"]
                                : Fill[3];
                            colors = new List<string>
                            {
                                c0, c1, c2, c3
                            };
                        }

                        break;
                    }
            }
            return colors;
        }
    }

    internal string SizeStr
    {
        get
        {
            if (Size.IsT0)
            {
                return Size.AsT0.ToString();
            }
            return Size.AsT1;
        }
    }
}


