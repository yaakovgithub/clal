namespace clal.Operations
{
    public interface IWorkFlowOperation
    {
        string Name { get; }
        double Execute(double input, long accountNumber, DateOnly currentDate);
    }
}
