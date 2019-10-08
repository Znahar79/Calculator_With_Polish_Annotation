using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class CloseBracketException:Exception
    {
        public CloseBracketException()
            : base(String.Format("Отсуствует открывающая скобка"))
        {
        }
    }
}
