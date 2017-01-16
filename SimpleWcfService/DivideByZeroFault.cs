using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SimpleWcfService
{
    [DataContract]
    public class DivideByZeroFault
    {
        private string info;

        public DivideByZeroFault(string info)
        {
            this.info = info;
        }

        [DataMember]
        public string Message
        {
            get
            {
                return this.info;
            }
            set
            {
                this.info = value;
            }
        }
    }
}
