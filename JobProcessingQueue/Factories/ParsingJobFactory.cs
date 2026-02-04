using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs;
using JobProcessingQueue.Jobs.Parsing;
using JobProcessingQueue.Jobs.Parsing.Parsers;

namespace JobProcessingQueue.Factories;

public class ParsingJobFactory : IJobFactory
{
    private readonly List<string> _fields;
    private readonly string _filePath;
    private readonly IFileParser _parser;
    private readonly Enums.Priority _priority;

    public Job CreateJob()
    {
        /* ... */
        return new ParsingJob();
    }
}