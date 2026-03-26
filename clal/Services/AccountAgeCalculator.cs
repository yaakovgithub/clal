namespace clal.Services
{
    public class AccountAgeCalculator : IAccountAgeCalculator
    {
        public TimeSpan Calculate(long accountNumber) => accountNumber switch
        {
            111111 => new TimeSpan(days: 356 * 5, hours: 0, minutes: 0, seconds: 0),
            222222 => new TimeSpan(days: 356 * 2, hours: 0, minutes: 0, seconds: 0),
            _ => new TimeSpan(days: Random.Shared.Next(356 * 10), hours: 0, minutes: 0, seconds: 0)
        };
    }
}
