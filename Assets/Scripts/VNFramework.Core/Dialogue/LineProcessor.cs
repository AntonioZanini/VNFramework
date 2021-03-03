using System;
using System.Collections.Generic;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class LineProcessor : ILineProcessor
    {
        private static readonly char[] separators = { '<', '>' };
        private List<LineSegment> segments = new List<LineSegment>();
        public IEnumerable<ILineSegment> Segments => segments;

        public void ProcessLine(string line)
        {
            var allRawSegments = SplitSegments(line);
            for (Int16 i = 0; i < allRawSegments.Length; i++)
            {
                segments.Add(new LineSegment(allRawSegments[i], IsATag(i)));
            }
        }

        private string[] SplitSegments(string line)
        {
            return line.Split(separators);
        }

        private bool IsATag(Int16 index)
        {
            return (index & 1) != 0;
        }
    }
}
