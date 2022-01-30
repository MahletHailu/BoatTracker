using System.Diagnostics.CodeAnalysis;

namespace BoatTrackerDomain.DataTransferObjects
{
    [ExcludeFromCodeCoverage]
    public class StateDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
    }
}
