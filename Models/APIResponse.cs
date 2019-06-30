using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class APIResponse
    {
        public bool Status { get; set; }

        public int StatusCode { get; set; }

        public string MessageCode { get; set; }

        public string Message { get; set; }

        public string Field { get; set; }

        public string LongMessage { get; set; }

        //public ArrayList DataAR { get; set; }

        public Dictionary<string, ArrayList> Data { get; set; }

        public ArrayList CreateList(params object[] items)
        {
            return new ArrayList(items);
        }
    }
}
