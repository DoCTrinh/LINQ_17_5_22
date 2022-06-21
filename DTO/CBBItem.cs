using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_17_5_22.DTO
{
    public class CBBItem
    {
        public int value { get; set; }
        public string text { get; set; }
        public override string ToString()
        {
            return text;
        }
    }
}
