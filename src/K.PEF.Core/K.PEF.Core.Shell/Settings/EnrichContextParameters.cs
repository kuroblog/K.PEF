using Serilog.Core;
using Serilog.Events;

namespace K.PEF.Core.Shell.Settings
{
    public class EnrichParameters
    {
        public const string TEST_ARG = "TestArg";
    }

    public class TestArgEnrich : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) =>
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(EnrichParameters.TEST_ARG, "This is a test arg."));
    }
}
