using System;
using System.Text.RegularExpressions;

namespace src
{
    class Program
    {

        static void Main(string[] args)
        {
            string input = "docker build -t myimage:latest .";
            Lexer lexer = new Lexer(input);
            lexer.PrintTokens();
        }
    }
}