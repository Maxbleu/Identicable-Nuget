using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identicable_Nuget.Interfaces
{
    public interface Iidentificable<T>
    {
        T Id { get; }
    }
}
