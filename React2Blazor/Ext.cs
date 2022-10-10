using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React2Blazor
{
    internal static class Ext
    {
        public static void AppendLine2(this StringBuilder sb, string line)
        {
            sb.Append(line);
            sb.Append(Environment.NewLine);
        }
    }
}
