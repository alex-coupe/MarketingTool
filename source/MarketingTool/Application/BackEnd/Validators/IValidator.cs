namespace BackEnd.Validators
{
    public interface IValidator<T>
    {
        bool Valid(T model);
    }
}
