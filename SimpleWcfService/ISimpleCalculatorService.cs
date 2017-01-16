using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SimpleWcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface ISimpleCalculatorService
    {
        [OperationContract]
        double Add(double a, double b);
        [OperationContract]
        double Substract(double a, double b);
        [OperationContract]
        double Multiply(double a, double b);
        [OperationContract]
        [FaultContract(typeof(DivideByZeroFault))]
        double Divide(double a, double b);
        [OperationContract]
        [FaultContract(typeof(RootOfNegativeNumberFault))]
        double Sqrt(double a);
    }
}
