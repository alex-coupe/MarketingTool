using DataTransfer.ViewModels;
using System.Collections.Generic;

namespace BackEnd.Validators
{
    public abstract class Validator<T>
    {
        protected List<Error> errors = new List<Error>();
        public abstract List<Error> ValidateModel(T model, Type type);
      
    }

    public enum Type
    {
        Post,
        Put,
    }
}
