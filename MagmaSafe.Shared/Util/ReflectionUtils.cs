using System;
using System.Linq;
using System.Reflection;

namespace MagmaSafe.Shared.Util
{
    public class ReflectionUtils
    {
        public static Type[] GetAllTypesThatImplementAnInterfaceg(Type interfaceType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(classType => interfaceType.IsAssignableFrom(classType)).ToArray();
        }

        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace, bool includeSubNameSpaces = false)
        {
            return assembly.GetTypes()
                      .Where(t => t.Namespace != null && (!includeSubNameSpaces ? t.Namespace.Equals(nameSpace) : t.Namespace.StartsWith(nameSpace + '.')))
                      .ToArray();
        }

        public static Type[] GetTypesOnSameNamespace(Type refType, bool includeSubNameSpaces = false, bool startFromParent = false)
        {
            var ns = refType.Namespace;
            if (startFromParent)
                ns = ns.Substring(0, ns.LastIndexOf('.'));

            return GetTypesInNamespace(Assembly.GetAssembly(refType), ns, includeSubNameSpaces);
        }

        public static void ForEachInterfaceClass(Type interfaceRefType, Type classRefType, Action<Type, Type> action, bool includeSubNameSpaces = false, bool startFromParent = false)
        {
            var interfaceTypes = ReflectionUtils.GetTypesOnSameNamespace(interfaceRefType, includeSubNameSpaces, startFromParent);
            var classTypes = ReflectionUtils.GetTypesOnSameNamespace(classRefType, includeSubNameSpaces, startFromParent);

            foreach (var it in interfaceTypes)
            {
                var ct = classTypes.FirstOrDefault(t => it.IsAssignableFrom(t));
                if (ct != null)
                {
                    action(it, ct);
                }
            }
        }
    }
}
