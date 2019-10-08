using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    [Serializable]
    class BracketException:Exception
    {
        public BracketException()
            : base(String.Format("Отсуствует закрывающая скобка"))
        {
        }

        public BracketException(string message)
            : base(message)
        {
        }

        public BracketException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
