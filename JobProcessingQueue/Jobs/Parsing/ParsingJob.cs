using JobProcessingQueue.Jobs.Parsing.Parsers;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Parsing;

public class ParsingJob : Job
{
    public string FilePath { get; set; }
    public List<string> Fields { get; set; }
    public IFileParser Parser { get; set; }

    public override async Task<IJobOutput> Execute()
    {
        /* ... */
        return await Task.FromResult(Parser.Parse(FilePath, Fields));
    }

    public override bool ProducesOutput()
    {
        return true;
    }
}