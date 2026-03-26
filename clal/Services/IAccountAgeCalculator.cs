namespace clal.Services
{
    public interface IAccountAgeCalculator
    {
        TimeSpan Calculate(long accountNumber);
    }
}
