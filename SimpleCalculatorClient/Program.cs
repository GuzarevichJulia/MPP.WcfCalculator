using System;
using System.Collections.Generic;

using SimpleCalculatorClient.СonsoleServiceReference;

using System.ServiceModel;

namespace SimpleCalculatorClient
{
    class Program
    {
        const int OPERATION_COUNT = 5;
        public delegate double BinaryOperation(double x, double y);
        public delegate double UnaryOperation(double x);

        static void Main(string[] args)
        {            
            SimpleCalculatorServiceClient client = new SimpleCalculatorServiceClient();
            
            Dictionary<int, Operation> availableOperations = OperationsInitializathion(client);

            double result = 0;
            int operationIndex = 0;
            double firstNumber = 0;
            double secondNumber = 0;         

            while (operationIndex != OPERATION_COUNT+1)
            {
                DisplayAvailableOperations(availableOperations);
                if (!int.TryParse(Console.ReadLine(), out operationIndex))
                {
                    OutputError("Specified operation doesn't exist");
                    continue;
                }

                if (!IsCorrect(operationIndex))
                {
                    OutputError("Specified operation doesn't exist");
                    continue;
                }

                if (operationIndex == OPERATION_COUNT + 1)
                {                
                    Console.WriteLine("Work of calculation is completed");
                    break;
                }

                Operation selectedOperation = availableOperations[operationIndex];

                Console.WriteLine("First value:");
                if (!double.TryParse(Console.ReadLine(), out firstNumber))
                {
                    OutputError("Value must be a number");
                    continue;
                }

                if (selectedOperation.TypeByArgCount == ArgumentsCount.Binary)
                {
                    Console.WriteLine("Second value:");
                    if (!double.TryParse(Console.ReadLine(), out secondNumber))
                    {
                        OutputError("Value must be a number");
                        continue;
                    }
                    try
                    {
                        BinaryOperation binaryOperation = (BinaryOperation)selectedOperation.Act;
                        result = binaryOperation(firstNumber, secondNumber);
                        Console.WriteLine("Result: {0}", result);
                    }
                    catch (FaultException<DivideByZeroFault> ex)
                    {
                        OutputError(ex.Detail.Message);
                    }
                }
                else
                {
                    try
                    {
                        UnaryOperation unaryOperation = (UnaryOperation)selectedOperation.Act;
                        result = unaryOperation(firstNumber);
                        Console.WriteLine("Result: {0}", result);
                    }
                    catch (FaultException<RootOfNegativeNumberFault> ex)
                    {
                        OutputError(ex.Detail.Message);
                    }
                }
                continue;
            }
            Console.ReadLine();
        }

        public static bool IsCorrect(int operationIndex)
        {
            return (operationIndex >= 1) && (operationIndex <= OPERATION_COUNT+1);
        }

        public static Dictionary<int, Operation> OperationsInitializathion(SimpleCalculatorServiceClient client)
        {
            Dictionary<int, Operation> availableOperations = new Dictionary<int, Operation>();

            availableOperations.Add(1, new Operation("ADD", new BinaryOperation(client.Add), ArgumentsCount.Binary));
            availableOperations.Add(2, new Operation("SUBSTRACT", new BinaryOperation(client.Substract), ArgumentsCount.Binary));
            availableOperations.Add(3, new Operation("MULTIPLY", new BinaryOperation(client.Multiply), ArgumentsCount.Binary));
            availableOperations.Add(4, new Operation("DIVIDE", new BinaryOperation(client.Divide), ArgumentsCount.Binary));
            availableOperations.Add(5, new Operation("SQRT", new UnaryOperation(client.Sqrt), ArgumentsCount.Unary));
            availableOperations.Add(6, new Operation("EXIT", null, 0));
            
            return availableOperations;
        }
        
        public static void DisplayAvailableOperations(Dictionary<int, Operation> availableOperation)
        {
            Console.WriteLine("--------------------------------------");
            for (int i = 1; i <= availableOperation.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i, availableOperation[i].Name);                
            }
            Console.WriteLine("Select on of the available operations:");
        }

        public static void OutputError(string info)
        {
            Console.WriteLine(info);
        }
    }
}
