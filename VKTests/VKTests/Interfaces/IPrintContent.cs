using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKTests
{
    public interface IPrintContent<T>
    {
        T CountElements { get; set; }
    }
}
