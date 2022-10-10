using System.ComponentModel;

namespace IconPark;

public enum StrokeLinecapType : byte
{
    [Description("butt")]
    Butt = 0,
    [Description("round")]
    Round = 1,
    [Description("square")]
    Square = 2
}

public enum StrokeLinejoinType : byte
{
    [Description("miter")]
    Miter = 0,
    [Description("round")]
    Round = 1,
    [Description("bevel")]
    Bevel = 2
}

public enum ThemeType : byte
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
