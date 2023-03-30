using System;
using System.Text.RegularExpressions;

namespace src
{
    class Program
    {

        static void Main(string[] args)
        {
            string input = "powershell start cmd --v \"runAs\" \"runAs sdf\"";
            Lexer lexer = new Lexer(input);
            lexer.PrintTokens();
        }
    }
}