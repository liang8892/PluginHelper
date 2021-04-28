using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Plugin.Interface;

namespace PluginHelper
{
    public class PluginHelper
    {
        public Dictionary<string, List<Type>> LoadPlugins(string pluginPath)
        {
            List<string> dlls = GetDlls(pluginPath);
            Dictionary<string, List<Type>> types = new Dictionary<string, List<Type>>();
            string interfaceFullName = typeof(NDInterfaces).FullName;
            if (string.IsNullOrEmpty(interfaceFullName))
                return types;
            foreach (string dll in dlls)
            {
                var temp = GetTypesImplementationInterface(dll, interfaceFullName);
                if (!string.IsNullOrEmpty(temp.Item1) && temp.Item2 != null && temp.Item2.Count > 0 && !types.Keys.Contains(temp.Item1))
                    types.Add(temp.Item1, temp.Item2);
            }
            return types;
        }


        private List<string> GetDlls(string pluginPath)
        {
            List<string> dlls = new List<string>();
            try
            {
                dlls = Directory.GetFiles(pluginPath, "*.dll", SearchOption.AllDirectories).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return dlls;
        }


        private (string, List<Type>) GetTypesImplementationInterface(string dll, string interfaceFullName)
        {
            try
            {
                Assembly asm = Assembly.LoadFile(dll);
                List<Type> types = new List<Type>();
                foreach (Type type in asm.GetTypes())
                {
                    if (type.GetInterface(interfaceFullName) != null)
                    {
                        types.Add(type);
                    }
                }
                return (asm.GetName().Name, types);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (string.Empty, null);
            }
        }

    }
}
