namespace clal.Services
{
    public class CreditStatusCalculator : ICreditStatusCalculator
    {
        public CreditStatus Calculate(long accountNumber) => accountNumber switch
        {
            111111 => CreditStatus.Good,
            222222 => CreditStatus.Bad,
            _ => CreditStatus.None
        };
    }
}
