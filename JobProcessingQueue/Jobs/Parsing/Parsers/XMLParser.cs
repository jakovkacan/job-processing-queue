using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Parsing.Parsers;

public class XMLParser : IFileParser
{
    public IJobOutput Parse(string filePath, List<string> fields)
    {
        /* ... */
        return new TabularOutput();
    }
}