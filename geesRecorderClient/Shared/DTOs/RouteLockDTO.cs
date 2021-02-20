namespace geesRecorderClient.Shared.DTOs
{
    public record RouteLockDTO
    {
        public bool Locked { get; set; }

        public string LockedRoute { get; set; }
    }
}