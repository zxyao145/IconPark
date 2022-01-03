using OneOf;

namespace IconPark;

public record SvgOptions
{
    public Theme Theme { get; init; } = Theme.Outline;

    public OneOf<int, string> Size { get; init; } = DefaultConfig.Size;

    public int StrokeWidth { get; init; } = DefaultConfig.StrokeWidth;

    public List<string> Fill { get; init; } = new List<string>();

    public StrokeLinecap StrokeLinecap { get; init; } = DefaultConfig.StrokeLinecap;

    public StrokeLinejoin StrokeLinejoin { get; init; } = DefaultConfig.StrokeLinejoin;

    public string Id { get; init; } = Guid.NewGuid().ToString();

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


