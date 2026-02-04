using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Parsing.Parsers;

public class JSONParser : IFileParser
{
    public IJobOutput Parse(string filePath, List<string> fields)
    {
        /* ... */
        return new TabularOutput();
    }
}