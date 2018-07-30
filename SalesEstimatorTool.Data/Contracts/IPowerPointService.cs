using System;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IPowerPointService
    {
        String ConvertToWordDocument(String pathToPresentation);
    }
}
