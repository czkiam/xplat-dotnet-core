using System;

namespace CheckListConsole
{
    public class LinkCheckResult
    {
        public int Id { get; set; }
        public bool Exists => String.IsNullOrWhiteSpace(Problem);
        public bool IsMissing => !Exists;
        public string Problem { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}