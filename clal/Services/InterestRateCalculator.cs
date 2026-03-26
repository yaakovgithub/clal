namespace clal.Services
{
    public class InterestRateCalculator : IInterestRateCalculator
    {
        public double Calculate(DateOnly effectiveDate)
 => effectiveDate < new DateOnly(2050, 1, 1) ? 0.0005 : 0.001;
    }
}
