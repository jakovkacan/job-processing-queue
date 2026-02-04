using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Parsing.Parsers;

public class CSVParser : IFileParser
{
    public IJobOutput Parse(string filePath, List<string> fields)
    {
        /* ... */
        return new TabularOutput();
    }
}