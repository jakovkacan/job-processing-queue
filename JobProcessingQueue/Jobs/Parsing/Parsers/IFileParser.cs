using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Parsing.Parsers;

public interface IFileParser
{
    IJobOutput Parse(string filePath, List<string> fields);
}