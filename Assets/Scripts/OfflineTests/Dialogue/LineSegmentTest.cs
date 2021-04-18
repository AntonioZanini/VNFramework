using NUnit.Framework;
using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Dialogue;

namespace Tests.Dialogue
{
    public class LineSegmentTest
    {

        [Test]
        public void LineSegment_ShouldReturnTagText_WhenReceiveTagFlag()
        {
            // ARRANGE
            const string TAG_CONTENT = "bold";
            ILineSegment lineSegment = new LineSegment(segmentText: TAG_CONTENT, segmentIsTag: true);

            // ACT
            string tagText = lineSegment.Text;

            // ASSERT
            Assert.AreEqual($"<{TAG_CONTENT}>", tagText);
        }

        [Test]
        public void LineSegment_ShouldReturnSimpleText_WhenReceiveTagFlagFalse()
        {
            // ARRANGE
            const string TAG_CONTENT = "bold";
            ILineSegment lineSegment = new LineSegment(segmentText: TAG_CONTENT, segmentIsTag: false);

            // ACT
            string tagText = lineSegment.Text;

            // ASSERT
            Assert.AreEqual(TAG_CONTENT, tagText);
        }

    }
}
