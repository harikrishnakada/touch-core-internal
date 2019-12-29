using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.ViewModel
{
    public interface IViewModel<in T>
    {
        //void Read(T entity);

        void Write(T entity);
    }
}
