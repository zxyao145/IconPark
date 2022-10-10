using System.ComponentModel;
using System.Reflection;


namespace IconPark.Svg
{
    internal static class Extension
    {

        public static string GetDescription<T>(this T value)
            where T : struct
        {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            string result = value.ToString();
            Type type = typeof(T);
            FieldInfo info = type.GetField(result!);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            var attributes = info!.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes != null && attributes.FirstOrDefault() != null)
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                result = (attributes.First() as DescriptionAttribute).Description;
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
            return result ?? "";
        }
    }
}
