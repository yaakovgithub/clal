using clal.Models;
using clal.Operations;
using clal.Services;
using Xunit;
namespace clal.Tests
{
    public class EngineTests
    {
        private readonly IWorkFlowEngine _engine;

        public EngineTests()
        {
            var operations = new List<IWorkFlowOperation>
        {
            new HolidayDiscountOperation(),
            new InterestRateOperation(new InterestRateCalculator()),
            new CreditStatusOperation(new CreditStatusCalculator()),
            new AccountAgeOperation(new AccountAgeCalculator())
        };

            _engine = new WorkFlowEngine(operations);
        }

        [Fact]
        public void Should_Stop_When_NextId_Is_Zero()
        {
            var request = new WorkFlowRequestDto
            {
                Account = new AccountDto
                {
                    AccountNumber = 111111,
                    InitialAmount = 100
                },
                Steps = new List<WorkFlowStepDto>
            {
                new()
                {
                    Id = 1,
                    OperationName = "credit-status",
                    NextIdIfOutputIsLessThan = 0,
                    NextIdIfOutputIsGreaterThan = 0
                }
            }
            };

            var result = _engine.Run(request, new DateOnly(2026, 3, 1));

            Assert.Equal(98, result.FinalAmount, 3);
            Assert.Single(result.ExecutedStepIds);
            Assert.Equal("Stopped Because ID is 0", result.StopReason);
        }

        [Fact]
        public void Should_Use_LessThan_1000_Branch()
        {
            var request = new WorkFlowRequestDto
            {
                Account = new AccountDto
                {
                    AccountNumber = 111111,
                    InitialAmount = 500
                },
                Steps = new List<WorkFlowStepDto>
            {
                new()
                {
                    Id = 1,
                    OperationName = "credit-status",
                    NextIdIfOutputIsLessThan = 2,
                    NextIdIfOutputIsGreaterThan = 3
                },
                new()
                {
                    Id = 2,
                    OperationName = "interest-rate",
                    NextIdIfOutputIsLessThan = 0,
                    NextIdIfOutputIsGreaterThan = 0
                },
                new()
                {
                    Id = 3,
                    OperationName = "account-age",
                    NextIdIfOutputIsLessThan = 0,
                    NextIdIfOutputIsGreaterThan = 0
                }
            }
            };

            var result = _engine.Run(request, new DateOnly(2026, 3, 1));

            Assert.Equal(new List<int> { 1, 2 }, result.ExecutedStepIds);
        }

        [Fact]
        public void Should_Stop_When_Output_Reaches_2000_Or_More()
        {
            var request = new WorkFlowRequestDto
            {
                Account = new AccountDto
                {
                    AccountNumber = 222222,
                    InitialAmount = 1999
                },
                Steps = new List<WorkFlowStepDto>
            {
                new()
                {
                    Id = 1,
                    OperationName = "credit-status",
                    NextIdIfOutputIsLessThan = 2,
                    NextIdIfOutputIsGreaterThan = 2
                },
                new()
                {
                    Id = 2,
                    OperationName = "interest-rate",
                    NextIdIfOutputIsLessThan = 0,
                    NextIdIfOutputIsGreaterThan = 0
                }
            }
            };

            var result = _engine.Run(request, new DateOnly(2026, 3, 1));

            Assert.True(result.FinalAmount >= 2000);
            Assert.Equal("Stopped because output was >= 2000 or <= 0", result.StopReason);
        }

        [Fact]
        public void Should_Apply_Holiday_Discount_In_September()
        {
            var request = new WorkFlowRequestDto
            {
                Account = new AccountDto
                {
                    AccountNumber = 123456,
                    InitialAmount = 1000
                },
                Steps = new List<WorkFlowStepDto>
            {
                new()
                {
                    Id = 1,
                    OperationName = "holiday-discount",
                    NextIdIfOutputIsLessThan = 0,
                    NextIdIfOutputIsGreaterThan = 0
                }
            }
            };

            var result = _engine.Run(request, new DateOnly(2026, 9, 15));

            Assert.Equal(950, result.FinalAmount, 3);
        }
    }
}
