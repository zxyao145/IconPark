using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace IconPark.Core
{
    [Generator]
    public class IconSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {

            // Get all classes
            var allNodes = context.Compilation
                .SyntaxTrees.SelectMany(s => s.GetRoot().DescendantNodes())
                .ToList();
            var allClasses = allNodes
                .Where(d => d.IsKind(SyntaxKind.ClassDeclaration))
                .OfType<ClassDeclarationSyntax>().ToList();

            foreach (ClassDeclarationSyntax classDef in allClasses)
            {
                if(classDef.BaseList != null)
                {
                    var baseType = classDef.BaseList.Types.FirstOrDefault();
                    if(baseType != null)
                    {
                        var baseTypeName = baseType.Type.ToString();
                        if (baseTypeName == "SvgComponentBase")
                        {
                            var componentName = classDef.Identifier.Text;
                            var sourceCode = GetComponentSource(componentName);
                            context.AddSource(componentName + ".razor.cs", sourceCode);
                        }
                    }
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }


        private static string GetComponentSource(string componentName)
        {
            string sourceCode = @"using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace IconPark;

public partial class ACane : IconParkComponmentBase
{
	protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
	{
		__builder.OpenElement(0, ""span"");
        __builder.OpenComponent<IconPark.Svg." + componentName + @">(1);
        __builder.AddAttribute(2, ""Props"", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<IconPark.SvgOptions>(this.GetSvgOptions()
        ));
        __builder.CloseComponent();
        __builder.CloseElement();
    }
}";
            return sourceCode;
        }
    }
}