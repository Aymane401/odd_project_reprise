using System;
using System.Collections;
using System.Reflection;

namespace App_UI.Helpers
{
    public static class ObjectExtensions
    {
        public static bool MemberCompare(this object left, object right )
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left == null || right == null)
                return false;

            Type type = left.GetType();
            if (type != right.GetType())
                return false;

            if (left as ValueType != null)
            {
                // do a field comparison, or use the override if Equals is implemented:
                return left.Equals(right);
            }

            // check for override:
            if (type != typeof(object)
                && type == type.GetMethod("Equals").DeclaringType)
            {
                // the Equals method is overridden, use it:
                return left.Equals(right);
            }

            // all Arrays, Lists, IEnumerable<> etc implement IEnumerable
            if (left as IEnumerable != null)
            {
                IEnumerator rightEnumerator = (right as IEnumerable).GetEnumerator();
                rightEnumerator.Reset();
                foreach (object leftItem in left as IEnumerable)
                {
                    // unequal amount of items
                    if (!rightEnumerator.MoveNext())
                        return false;
                    else
                    {
                        if (!MemberCompare(leftItem, rightEnumerator.Current))
                            return false;
                    }
                }
            }
            else
            {
                // compare each property
                foreach (PropertyInfo info in type.GetProperties(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.GetProperty))
                {
                    // xTODO: need to special-case indexable properties
                    if (!MemberCompare(info.GetValue(left, null), info.GetValue(right, null)))
                        return false;
                }

                // compare each field
                foreach (FieldInfo info in type.GetFields(
                    BindingFlags.GetField |
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Instance))
                {
                    if (!MemberCompare(info.GetValue(left), info.GetValue(right)))
                        return false;
                }
            }
            return true;
        }
    }
}
