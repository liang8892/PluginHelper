using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Plugin.Interface;
using PluginHelper;

namespace Test.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new PluginHelper.PluginHelper();
            var types = helper.LoadPlugins(Path.Combine(Environment.CurrentDirectory, "plugins"));
            foreach (KeyValuePair<string, List<Type>> pair in types)
            {
                foreach (Type type in pair.Value)
                {
                    NDInterfaces obj = (NDInterfaces)Activator.CreateInstance(type);
                    obj.TestMethod();
                }
            }

            Console.ReadLine();
        }
    }
}
