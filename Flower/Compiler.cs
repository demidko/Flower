using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows;

// TODO: рекурсивность

namespace Flower
{
    static class Compiler
    {
        private const string Begin = @"using System; 
                                       
                                       public static class LambdaCreator
                                       {
                                           const double e = Math.E;
                                           const double pi = Math.PI;
                                           static readonly Func<double, double, double> pow = Math.Pow;
                                           public static Func<double, double> Create() { return (x)=>";
        private const string End = @";}}";

        private static CompilerResults Compile(in string middle)
        {
            CompilerParameters parameters = new CompilerParameters()
            {
                GenerateInMemory = true
            };
            parameters.ReferencedAssemblies.Add("System.dll");
            return new CSharpCodeProvider().CompileAssemblyFromSource(parameters, Begin + middle + End);
        }

        public static Func<double, double> CompileMathFunc(in string line)
        {
            var buildResults = Compile(line);
            if (buildResults.Errors.Count > 0)
            {
                var e = buildResults.Errors[0];
                MessageBox.Show($"Ошибка начинается с символа {e.Column-101}:\n{e.ErrorText}", "Функция записана неправильно!");
                return null;
            }
            return buildResults
                .CompiledAssembly
                .GetType("LambdaCreator")
                .GetMethod("Create", BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, null) as Func<double, double>;
        }
    }
}
