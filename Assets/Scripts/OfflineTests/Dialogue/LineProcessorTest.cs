using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Dialogue;

namespace Tests.Dialogue
{
    public class LineProcessorTest
    {
        private class MockLineSegment : ILineSegment
        {

            public bool IsTag { get; private set; }
            public string Text { get; private set; }
            public string DisplayText => IsTag ? $"<{Text}>" : Text;

            public MockLineSegment(string segmentText, bool segmentIsTag)
            {
                Text = segmentText;
                IsTag = segmentIsTag;
            }
        }

        [Test]
        public void LineProcessor_ShouldReturn7Segments_WhenReceiveLineWith4Tags()
        {
            // ARRANGE
            ILineProcessor lineProcessor = new LineProcessor<MockLineSegment>();
            string lineText = "<bold>My text </bold> have <s> 7 segments</s>";

            // ACT
            lineProcessor.ProcessLine(lineText);

            // ASSERT
            Assert.AreEqual(9, lineProcessor.Segments.Count());
        }
        
        [Test]
        public void LineProcessor_ShouldReturn9Segments_WhenReceiveLineWith4TagsSurroundedByWhiteSpaces()
        {
            // ARRANGE
            ILineProcessor lineProcessor = new LineProcessor<MockLineSegment>();
            string lineText = "   <bold>My text </bold> have <s> 9 segments</s>   ";

            // ACT
            lineProcessor.ProcessLine(lineText);

            // ASSERT
            Assert.AreEqual(9, lineProcessor.Segments.Count());
        }

        [Test]
        public void LineProcessor_ShouldReturn9Segments_WhenReceiveLineWith4InnerTags()
        {
            // ARRANGE
            ILineProcessor lineProcessor = new LineProcessor<MockLineSegment>();
            string lineText = "My <bold> text </bold> have <s> 9 </s> segments";

            // ACT
            lineProcessor.ProcessLine(lineText);

            // ASSERT
            Assert.AreEqual(9, lineProcessor.Segments.Count());
        }

        [Test]
        public void LineProcessor_ShouldReturn4TagSegments_WhenReceiveLineWith4Tags()
        {
            // ARRANGE
            ILineProcessor lineProcessor = new LineProcessor<MockLineSegment>();
            string lineText = "My <bold> text </bold> have <s> 4 </s> tags";

            // ACT
            lineProcessor.ProcessLine(lineText);

            // ASSERT
            Assert.AreEqual(4, lineProcessor.Segments.Count(s => s.IsTag));
        }
        
        [Test]
        public void LineProcessor_ShouldReturnSegmentsEquivalentToTheOriginalText()
        {
            // ARRANGE
            ILineProcessor lineProcessor = new LineProcessor<MockLineSegment>();
            string lineText = "My <bold> text </bold> have <s> 4 </s> tags";

            // ACT
            lineProcessor.ProcessLine(lineText);

            // ASSERT
            Assert.AreEqual(lineText, string.Join(string.Empty, lineProcessor.Segments.Select(s => s.DisplayText)));
        }
             
        [Test]
        public void LineProcessor_ShouldReturnResultsOfSecondProcessing_WhenProcessTwoLinesOnRow()
        {
            // ARRANGE
            ILineProcessor lineProcessor = new LineProcessor<MockLineSegment>();
            string lineText1 = "My <bold> text </bold> have <s> 4 </s> tags";
            string lineText2 = "<bold>My text </bold> <u> have </u> <s> 7 segments</s>";

            // ACT
            lineProcessor.ProcessLine(lineText1);
            lineProcessor.ProcessLine(lineText2);

            // ASSERT
            Assert.AreEqual(lineText2, string.Join(string.Empty, lineProcessor.Segments.Select(s => s.DisplayText)));
            Assert.AreEqual(13, lineProcessor.Segments.Count());
            Assert.AreEqual(6, lineProcessor.Segments.Count(s => s.IsTag));
        }

    }
}