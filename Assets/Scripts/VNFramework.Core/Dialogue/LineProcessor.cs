using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class LineProcessor<T> : ILineProcessor where T : ILineSegment 
    {
        ConstructorInfo typeConstructor;
        private static readonly char[] separators = { '<', '>' };
        private List<T> segments = new List<T>();
        public IEnumerable<ILineSegment> Segments => segments.Cast<ILineSegment>();

        public LineProcessor()
        {
            typeConstructor = typeof(T).GetConstructor(
                types: new Type[] { typeof(string), typeof(bool)
            });
        }

        public void ProcessLine(string line)
        {
            string[] allRawSegments = SplitSegments(line);
            segments.Clear();
            for (Int16 i = 0; i < allRawSegments.Length; i++)
            {
                segments.Add(NewLineSegment(tagText: allRawSegments[i], isATag: IsATag(i)));
            }
        }

        private T NewLineSegment(string tagText, bool isATag)
        {
            return (T)typeConstructor.Invoke(new object[] { tagText, isATag });
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
