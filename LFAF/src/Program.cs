using System;
using System.Text.RegularExpressions;
using ASTNodes;
using ParserNamespace;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "git checkout -b master";
            var parser = new GitCommandParser(input);
            ASTNode root = parser.Parse();

            string jsonString = root.ToJsonString();
            Console.WriteLine(jsonString);
        }
    }
}