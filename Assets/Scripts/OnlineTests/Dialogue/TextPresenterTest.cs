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
                speech: DialogueTestHelpers.GetMockSpeech(speechText, DialogueTestHelpers.GetMockSpeechSettings()),
                coroutineAccessor: DialogueTestHelpers.GetMockCoroutineAccessor(),
                lineProcessor: DialogueTestHelpers.GetMockLineProcessor(lineSegments),
                preppendText: string.Empty);

            // ACT
            textPresenter.Present();
            while (textPresenter.IsPresenting) { yield return new WaitForFixedUpdate(); }

            // ASSERT
            Assert.AreEqual(speechText, textPresenter.CurrentText);
        }

    }
}
