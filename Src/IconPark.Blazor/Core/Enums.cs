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
