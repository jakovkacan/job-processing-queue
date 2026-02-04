using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs;
using JobProcessingQueue.Jobs.Code;

namespace JobProcessingQueue.Factories;

public class CodeJobFactory : IJobFactory
{
    private readonly Enums.ProgrammingLanguageType _languageType;
    private readonly Enums.Priority _priority;
    private readonly string _sourceCode;

    public Job CreateJob()
    {
        /* ... */
        return new CodeJob();
    }
}