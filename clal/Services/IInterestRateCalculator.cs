namespace clal.Services
{
    /// <summary> calculate interest rate (used by operation: interest-rate) </summary>
    public interface IInterestRateCalculator
    {
        double Calculate(DateOnly effectiveDate);
    }
}
