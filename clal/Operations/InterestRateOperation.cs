using clal.Services;

namespace clal.Operations
{
    public class InterestRateOperation : IWorkFlowOperation
    {
        public string Name => "interest-rate";

        private readonly IInterestRateCalculator _interestRateCalculator;

        public InterestRateOperation(IInterestRateCalculator interestRateCalculator)
        {
            _interestRateCalculator = interestRateCalculator;
        }

        public double Execute(double input, long accountNumber, DateOnly currentDate)
        {
            var rate = 1+_interestRateCalculator.Calculate(currentDate);
            return input * rate;
        }
    }
}
