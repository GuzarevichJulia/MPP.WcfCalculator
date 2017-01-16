using System;

namespace SimpleCalculatorClient
{
    public class Operation
    {
        public Delegate Act { get; private set; }
        public string Name { get; private set; }
        public ArgumentsCount TypeByArgCount { get; set; }

        public Operation(string name, Delegate act, ArgumentsCount type)
        {
            Name = name;
            Act = act;
            TypeByArgCount = type;
        }
    }

    public enum ArgumentsCount
    {
        Unary,
        Binary
    }
}
