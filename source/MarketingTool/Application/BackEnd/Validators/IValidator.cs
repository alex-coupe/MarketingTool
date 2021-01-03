using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validators
{
    public interface IValidator<T>
    {
        bool Valid(T type);
    }
}
