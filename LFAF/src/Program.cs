using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            Grammar grammar = new Grammar();
            FiniteAutomaton finiteAutomaton = grammar.toFiniteAutomaton();
            Console.WriteLine("Hello World!");
        }
    }
}