using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Projeto_RazorPage_Padaria.Enumerations.Utilities
{
    public static class EnumUtilities
    {
        public static IEnumerable<SelectListItem> GetSelectList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.GetDisplayName()
                });
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var fieldInfo = type.GetField(enumValue.ToString());
            var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute != null ? displayAttribute.Name : enumValue.ToString();
        }
    }
}
