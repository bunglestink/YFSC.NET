using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

internal static class ExtensionMethods {
    public static bool DoesAnyKeyHavePrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix) {
        return FindKeysWithPrefix<TValue>(dictionary, prefix).Any<KeyValuePair<string, TValue>>();
    }

    public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix) {
        TValue exactMatchValue;
        if (dictionary.TryGetValue(prefix, out exactMatchValue)) {
            yield return new KeyValuePair<string, TValue>(prefix, exactMatchValue);
        }

        foreach (var entry in dictionary) {
            string key = entry.Key;

            if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) {
                switch (key[prefix.Length]) {
                    case '[':
                    case '.':
                        yield return entry;
                        break;
                }
            }
        }
    }

    public static TAttribute GetAttribute<TAttribute>(this MemberDescriptor member) where TAttribute : Attribute {
        return member.Attributes.OfType<TAttribute>().FirstOrDefault();
    }

    public static TAttribute GetAttribute<TAttribute>(this ICustomTypeDescriptor typeDescriptor) where TAttribute : Attribute {
        return typeDescriptor.GetAttributes().OfType<TAttribute>().FirstOrDefault();
    }

    public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberDescriptor member) where TAttribute : Attribute {
        return member.Attributes.OfType<TAttribute>();
    }

    public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomTypeDescriptor typeDescriptor) where TAttribute : Attribute {
        return typeDescriptor.GetAttributes().OfType<TAttribute>();
    }

    public static bool IsNullable(this Type type) {
        if (!type.IsValueType) {
            return true;
        }

        return (type.IsGenericType && !type.IsGenericTypeDefinition && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
    }
}
