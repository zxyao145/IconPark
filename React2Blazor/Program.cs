
using React2Blazor;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;


var baseDir = Path.Combine(AppContext.BaseDirectory, "../../../../");
var outputComponentDir = baseDir + "Src\\IconPark.Blazor\\Svg\\Component\\" ;
void GenerateComponent()
{
    string reactSrcIcons = baseDir + "IconPark\\packages\\react\\src\\icons";

    Regex idRegex = new Regex(@"props.id[ ]*\+[ ]*\'[\d\w]+'");
    var tsxs = Directory.GetFiles(reactSrcIcons, "*.tsx");
    var count = tsxs.Length;
    var index = 1;
    foreach (var tsx in tsxs)
    {
        var componentName = Path.GetFileNameWithoutExtension(tsx);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine2("@inherits SvgComponentBase");
        sb.AppendLine2("");
        sb.AppendLine2("@*");

        using var fileStream = new FileStream(tsx, FileMode.Open);
        using var sr = new StreamReader(fileStream);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            line = line.Trim();

            // 注释
            if (line.StartsWith("/*"))
            {
                sb.AppendLine2(line.Replace("@", ""));

                if (!line.EndsWith("*/"))
                {
                    line = sr.ReadLine();
                    while (!line!.EndsWith("*/"))
                    {
                        sb.AppendLine2(line);
                        line = sr.ReadLine();
                    }
                    sb.AppendLine2(line.Replace("@", ""));
                }
            }


            // 
            if (line.StartsWith("<svg"))
            {
                sb.AppendLine2("*@");
                sb.AppendLine2("");

                while (!line!.EndsWith("</svg>"))
                {
                    if (idRegex.Match(line).Success)
                    {
                        if (line.Contains("url(#"))
                        {
                            line = "                clipPath=@($\"url(#{Props.Id})\")";
                        }
                        else
                        {
                            line = "                id=@($\"url(#{Props.Id})\")";
                        }
                    }
                    line = line.Replace("{props.size}", "@Props.SizeStr")
                        .Replace("{props.colors[0]}", "@Props.Colors[0]")
                        .Replace("{props.colors[1]}", "@Props.Colors[1]")
                        .Replace("{props.colors[2]}", "@Props.Colors[2]")
                        .Replace("{props.colors[3]}", "@Props.Colors[3]")
                        .Replace("{props.strokeWidth}", "@Props.StrokeWidth")
                        .Replace("{props.strokeLinecap}", "@Props.StrokeLinecap.GetDescription()")
                        .Replace("{props.strokeLinejoin}", "@Props.StrokeLinejoin.GetDescription()")
                        .Replace("strokeWidth", "stroke-width")
                        .Replace("strokeLinejoin", "stroke-linejoin")
                        .Replace("strokeLinecap", "stroke-linecap")
                        ;

                    sb.AppendLine2(line);

                    line = sr.ReadLine();
                }
                sb.AppendLine2(line);
                break;
            }
        }

        if(componentName == "System")
        {
            componentName = "SystemIcon";
        }

        var newFilePath = outputComponentDir + componentName + ".razor";
        var newDir = Path.GetDirectoryName(newFilePath);
        if (!Directory.Exists(newDir))
        {
            Directory.CreateDirectory(newDir!);
        }

        using var sw = new StreamWriter(newFilePath);
        sw.WriteLine(sb.ToString());
        Console.Write($"\r{index++ } / {count}");
    }
}

GenerateComponent();

var demoPath = baseDir + "Sample\\IconPark.Blazor.Sample\\Pages\\FullIcons.razor";

void GenerateDemo()
{
    var allComponments = Directory.GetFiles(outputComponentDir, "*.razor");
    var sb = new StringBuilder("@page \"/FullIcons\"" + Environment.NewLine + Environment.NewLine);
    foreach (var componment in allComponments)
    {
        var name = Path.GetFileNameWithoutExtension(componment);
        var demoText = $"<IconPark.{name} />";
        var demo = $@"<BiCell Name=""{name}"" Code=""{HtmlEncoder.Default.Encode(demoText)}"">
    <IconPark.{name} Size=""24"" />
</BiCell>";
        sb.AppendLine2(demo);
    }

    using var sw = new StreamWriter(demoPath);
    sw.WriteLine(sb.ToString());
}

GenerateDemo();

Console.WriteLine();
Console.WriteLine("Finished!");
