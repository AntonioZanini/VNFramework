﻿using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Global;

namespace Tests.Dialogue
{
    internal class DialogueTestHelpers
    {
        internal class MockCoroutineAccessor : MonoBehaviour, ICoroutineAccessor { }

        internal static ICoroutineAccessor GetMockCoroutineAccessor()
        {
            GameObject gameObject = new GameObject();
            ICoroutineAccessor coroutineAccessor = gameObject.AddComponent<MockCoroutineAccessor>();
            return coroutineAccessor;
        }

        internal static ISpeech GetMockSpeech(string speechText, ISpeechSettings speechSettings)
        {
            ISpeech speech = Substitute.For<ISpeech>();
            speech.AdditiveSpeech.Returns(true);
            speech.SpeechText.Returns(speechText);
            speech.SpeakerName.Returns("Main Character");
            speech.SpeechSettings.Returns(speechSettings);
            return speech;
        }

        internal static ISpeechSettings GetMockSpeechSettings()
        {
            ISpeechSettings speechSettings = Substitute.For<ISpeechSettings>();
            speechSettings.DisplaySpeed.Returns(5f);
            speechSettings.Font.Returns(new Font("Arial"));
            speechSettings.FontColor.Returns(Color.white);
            speechSettings.FontSize.Returns(45);
            speechSettings.FontOnSpeakerName.Returns(false);
            return speechSettings;
        }

        internal static ILineSegment GetMockLineSegment(string text, bool isATag = false)
        {
            ILineSegment lineSegment = Substitute.For<ILineSegment>();
            lineSegment.Text.Returns(text);
            lineSegment.IsTag.Returns(isATag);
            return lineSegment;
        }
        internal static ILineProcessor GetMockLineProcessor(IEnumerable<ILineSegment> lineSegments)
        {
            ILineProcessor lineProcessor = Substitute.For<ILineProcessor>();
            lineProcessor.Segments.Returns(lineSegments);
            return lineProcessor;
        }

        internal static IEnumerable<ILineSegment> GetListSegments(IEnumerable<string> segmentTexts)
        {
            List<ILineSegment> lineSegments = new List<ILineSegment>();
            foreach (string segmentText in segmentTexts)
            {
                bool isATag = segmentText.StartsWith("<") && segmentText.EndsWith(">");
                lineSegments.Add(GetMockLineSegment(segmentText, isATag));
            }
            return lineSegments;
        }
    }
}