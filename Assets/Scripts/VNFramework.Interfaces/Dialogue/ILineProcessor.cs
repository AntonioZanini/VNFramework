using System.Collections.Generic;

namespace VNFramework.Interfaces.Dialogue
{
    public interface ILineProcessor
    {
        IEnumerable<ILineSegment> Segments { get; }
        void ProcessLine(string line);
    }
}
