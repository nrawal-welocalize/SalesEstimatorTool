using System;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IWordService
    {
        WordComputeStatistic GetComputeStatistic(String path);
    }
}
