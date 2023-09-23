using System;
using UnityEngine;

namespace MetaX
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InteractiveAttribute : PropertyAttribute
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public InteractiveAttribute() { }
    }
}

#if UNITY_EDITOR
namespace MetaX.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Object = UnityEngine.Object;

    public class InteractiveHandler
    {
        /* Variables */

        public readonly List<(MethodInfo Method, string Name)> TargetMethods;
        public int Amount => TargetMethods?.Count ?? 0;

        private readonly Object _target;

        /* Setter & Getter */

        /* Functions */

        public InteractiveHandler(Object target)
        {
            _target = target;

            var type = target.GetType();
            var bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var members = type.GetMembers(bindings).Where(IsInteractiveMethod);

            foreach (var member in members)
            {
                var method = member as MethodInfo;
                if (method == null) continue;

                if (IsValidMember(method, member))
                {
                    var attribute = (InteractiveAttribute)Attribute.GetCustomAttribute(method, typeof(InteractiveAttribute));
                    if (TargetMethods == null) 
                        TargetMethods = new List<(MethodInfo, string)>();
                    TargetMethods.Add((method, method.Name));
                }
            }
        }

        private bool IsInteractiveMethod(MemberInfo memberInfo)
        {
            return Attribute.IsDefined(memberInfo, typeof(InteractiveAttribute));
        }

        private bool IsValidMember(MethodInfo method, MemberInfo member)
        {
            if (method == null)
            {
                Debug.LogWarning(
                    $"Property <color=brown>{member.Name}</color>.Reason: Member is not a method but has InteractiveAttribute!");
                return false;
            }

            // TODO: Support parameters?
            if (method.GetParameters().Length > 0)
            {
                Debug.LogWarning(
                    $"Method <color=brown>{method.Name}</color>.Reason: Methods with parameters is not supported by InteractiveAttribute!");
                return false;
            }

            return true;
        }
    }
}
#endif
