using DataTransfer.ViewModels;
using System.Collections.Generic;

namespace BackEnd.Validators
{
    public abstract class Validator<T>
    {
        protected List<Error> errors;
        public abstract List<Error> ValidateModel(T model);
      
    }
}
