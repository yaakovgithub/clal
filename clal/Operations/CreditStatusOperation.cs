
using clal.Services;

namespace clal.Operations
{
    public class CreditStatusOperation : IWorkFlowOperation
    {
        public string Name => "credit-status";

        private readonly ICreditStatusCalculator _creditStatusCalculator;

        public CreditStatusOperation(ICreditStatusCalculator creditStatusCalculator)
        {
            _creditStatusCalculator = creditStatusCalculator;
        }

        public double Execute(double input, long accountNumber, DateOnly currentDate)
        {
            var status = _creditStatusCalculator.Calculate(accountNumber);
            var rate = status switch { CreditStatus.Good => 0.98, CreditStatus.Bad => 1.02, _ => 1 };
            return rate* input;
        }
    }
}
