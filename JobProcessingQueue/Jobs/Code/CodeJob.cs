using JobProcessingQueue.Core;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Code;

public class CodeJob : Job, IStoppable
{
    public string SourceCode { get; set; }
    public Enums.ProgrammingLanguageType LanguageType { get; set; }
    public List<string> Arguments { get; set; }

    public void Stop()
    {
        /* ... */
    }

    public override async Task<IJobOutput> Execute()
    {
        /* ... */
        return new TextOutput();
    }

    public override bool ProducesOutput()
    {
        return true;
    }
}