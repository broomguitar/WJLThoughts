using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJLThoughts.Common.Core.Auxs
{
    public class ReflectionHelper
    {
        /// <summary>
        /// 通过反射加载程序集中所有T以及T派生类型(抽象类除外)实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> CreateAllInstancesOf<T>()
        {
            return typeof(T).Assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract && t.IsClass)
                .Select(t => (T)Activator.CreateInstance(t));
        }
        /// <summary>
        /// 通过反射加载程序集中所有T以及T派生类型(抽象类除外)实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> CreateAllInstancesOf<T>(System.Reflection.Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract && t.IsClass)
                .Select(t => (T)Activator.CreateInstance(t));
        }
    }

}
