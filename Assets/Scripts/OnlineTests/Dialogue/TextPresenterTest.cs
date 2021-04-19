using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Dialogue;

namespace Tests.Dialogue
{
    public class TextPresenterTest
    {
        [UnityTest]
        public IEnumerator TextPresenter_ShouldReturnSameTextAsInputed_WhenAfterPresenting()
        {
            // ARRANGE
            string speechText = "My <bold> text </bold> have <s> 9 </s> segments";
            IEnumerable<ILineSegment> lineSegments = DialogueTestHelpers.GetListSegments(
                new [] { "My ", "<bold>", " text ", "</bold>", " have ", "<s>", " 9 ", "</s>", " segments" });

            ITextPresenter textPresenter = new TextPresenter(
                coroutineAccessor: DialogueTestHelpers.GetMockCoroutineAccessor(),
                lineProcessor: DialogueTestHelpers.GetMockLineProcessor(lineSegments));

            textPresenter.Initialize(
                speech: DialogueTestHelpers.GetMockSpeech(speechText, DialogueTestHelpers.GetMockSpeechSettings()),
                preppendText: string.Empty);

            // ACT
            textPresenter.Present();
            while (textPresenter.IsPresenting) { yield return new WaitForFixedUpdate(); }

            // ASSERT
            Assert.AreEqual(speechText, textPresenter.CurrentText);
        }

        [UnityTest]
        public IEnumerator TextPresenter_ShouldReturnSameTextAsInputed_WhenSkipIsActivated()
        {
            // ARRANGE
            string speechText = "My <bold> text </bold> have <s> 9 </s> segments";
            IEnumerable<ILineSegment> lineSegments = DialogueTestHelpers.GetListSegments(
                new[] { "My ", "<bold>", " text ", "</bold>", " have ", "<s>", " 9 ", "</s>", " segments" });

            ITextPresenter textPresenter = new TextPresenter(
                coroutineAccessor: DialogueTestHelpers.GetMockCoroutineAccessor(),
                lineProcessor: DialogueTestHelpers.GetMockLineProcessor(lineSegments));

            textPresenter.Initialize(
                speech: DialogueTestHelpers.GetMockSpeech(speechText, DialogueTestHelpers.GetMockSpeechSettings()),
                preppendText: string.Empty);

            // ACT
            textPresenter.Present();
            textPresenter.Skip();
            while (textPresenter.IsPresenting) { yield return new WaitForFixedUpdate(); }

            // ASSERT
            Assert.AreEqual(speechText, textPresenter.CurrentText);
        }

        [UnityTest]
        public IEnumerator TextPresenter_ShouldReturnSameTextAsInputedPlusPreviousText_WhenAfterPresentingAndIsAdditiveText()
        {
            // ARRANGE
            string previousText = "Hello!";
            string speechText = " My <bold> text </bold> have <s> 9 </s> segments";
            string expectedOutput = previousText + speechText;
            IEnumerable<ILineSegment> lineSegments = DialogueTestHelpers.GetListSegments(
                new[] { " My ", "<bold>", " text ", "</bold>", " have ", "<s>", " 9 ", "</s>", " segments" });

            ITextPresenter textPresenter = new TextPresenter(
                coroutineAccessor: DialogueTestHelpers.GetMockCoroutineAccessor(),
                lineProcessor: DialogueTestHelpers.GetMockLineProcessor(lineSegments));

            textPresenter.Initialize(
                speech: DialogueTestHelpers.GetMockSpeech(speechText, DialogueTestHelpers.GetMockSpeechSettings()),
                preppendText: previousText);

            // ACT
            textPresenter.Present();
            while (textPresenter.IsPresenting) { yield return new WaitForFixedUpdate(); }

            // ASSERT
            Assert.AreEqual(expectedOutput, textPresenter.CurrentText);
        }

        [UnityTest]
        public IEnumerator TextPresenter_ShouldReturnSameTextAsInputedPlusPreviousText_WhenSkipIsActivatedAndIsAdditiveText()
        {
            // ARRANGE
            string previousText = "Hello!";
            string speechText = " My <bold> text </bold> have <s> 9 </s> segments";
            string expectedOutput = previousText + speechText;
            IEnumerable<ILineSegment> lineSegments = DialogueTestHelpers.GetListSegments(
                new[] { " My ", "<bold>", " text ", "</bold>", " have ", "<s>", " 9 ", "</s>", " segments" });

            ITextPresenter textPresenter = new TextPresenter(
                coroutineAccessor: DialogueTestHelpers.GetMockCoroutineAccessor(),
                lineProcessor: DialogueTestHelpers.GetMockLineProcessor(lineSegments));

            textPresenter.Initialize(
                speech: DialogueTestHelpers.GetMockSpeech(speechText, DialogueTestHelpers.GetMockSpeechSettings()),
                preppendText: previousText);

            // ACT
            textPresenter.Present();
            textPresenter.Skip();
            while (textPresenter.IsPresenting) { yield return new WaitForFixedUpdate(); }

            // ASSERT
            Assert.AreEqual(expectedOutput, textPresenter.CurrentText);
        }
    }
}
