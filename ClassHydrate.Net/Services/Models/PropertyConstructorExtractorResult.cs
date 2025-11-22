namespace ClassHydrate.Net.Services.Models
{
    public record PropertyConstructorExtractorResult
    {
        public required IEnumerable<object> Results { get; init; }
        public required IEnumerable<string> PropertyNames { get; init; }
    }
}
