using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Interface;
using PluginHelper;

namespace Test.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new PluginHelper.PluginHelper();
            helper.LoadPlugins(Path.Combine(Environment.CurrentDirectory, "plugins"));
        }
    }
}
