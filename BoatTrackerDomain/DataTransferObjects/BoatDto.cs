using System.Diagnostics.CodeAnalysis;

namespace BoatTrackerDomain.DataTransferObjects
{
    [ExcludeFromCodeCoverage]
    public class BoatDto
    {
        public string HIN { get; set; }
        public string Name { get; set; }
        [MaybeNull] public StateDto BoatState { get; set; }
    }
}
