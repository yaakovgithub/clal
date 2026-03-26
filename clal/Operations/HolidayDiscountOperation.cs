
namespace clal.Operations
{
    public class HolidayDiscountOperation : IWorkFlowOperation
    {
        public string Name => "holiday-discount";

        public double Execute(double input, long accountNumber, DateOnly currentDate)
        {
            if (currentDate.Month == 9)
            {
                return 0.95 * input;
            }
            return input;
        }
    }
}
