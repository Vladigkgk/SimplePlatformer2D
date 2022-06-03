using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace PixelCrew.Utils.Editor
{
    public static class SerializedPropertyExtension
    {
        public static bool GetEnums<TEnumType>(this SerializedProperty property, out TEnumType retValue) where TEnumType : Enum
        {
            retValue = default;
            var names = property.enumNames;

            if (names == null || names.Length == 0) return false;

            var enumName = names[property.enumValueIndex];
            retValue = (TEnumType) Enum.Parse(typeof(TEnumType), enumName);
            return true;
        }
    }
}
