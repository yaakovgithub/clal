namespace clal.Services
{
    public interface ICreditStatusCalculator
    {
        CreditStatus Calculate(long accountNumber);
    }
    public enum CreditStatus
    {
        None, Good, Bad
    }

}
