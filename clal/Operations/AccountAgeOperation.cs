using clal.Services;

namespace clal.Operations
{
    public class AccountAgeOperation : IWorkFlowOperation
    {
        private readonly int Year=365;
        private readonly IAccountAgeCalculator _accountAgeCalculator;
        public string Name => "account-age";

        public AccountAgeOperation(IAccountAgeCalculator accountAgeCalculator)
        {
            _accountAgeCalculator = accountAgeCalculator;
        }


        public double Execute(double input, long accountNumber, DateOnly currentDate)
        {
            var age = _accountAgeCalculator.Calculate(accountNumber);

            if (age.TotalDays > Year * 3)
            {
                return input * 0.90;
            }

            return input;
        }
    }
}
