using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class LineSegment : ILineSegment
    {
        private string text;
        public bool IsTag { get; private set; }
        public string DisplayText => IsTag ? $"<{text}>": text;

        public LineSegment(string segmentText, bool segmentIsTag)
        {
            text = segmentText;
            IsTag = segmentIsTag;
        }
    }
}
