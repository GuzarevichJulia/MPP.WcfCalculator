using System;
using System.ServiceModel;

namespace SimpleWcfService
{
    public class SimpleCalculatorService : ISimpleCalculatorService
    {
        public double Add(double a, double b)
        {
            return a + b;
        }
        public double Substract(double a, double b)
        {
            return a - b;
        }
        public double Multiply(double a, double b)
        {
            return a * b;
        }
        public double Divide(double a, double b)
        {
            if(b == 0)
            {
                throw new FaultException<DivideByZeroFault>(new DivideByZeroFault("Second value can't be \"0\""));
            }
            return a / b;
        }
        public double Sqrt(double a)
        {
            if(a < 0)
            {
                throw new FaultException<RootOfNegativeNumberFault>(new RootOfNegativeNumberFault("Input value can't be negative"));
            }
            return Math.Sqrt(a);
        }
    }
}
