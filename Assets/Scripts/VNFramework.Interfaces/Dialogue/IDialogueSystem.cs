﻿namespace VNFramework.Interfaces.Dialogue
{
    public interface IDialogueSystem
    {
        IDialogueSystemElements Elements { get; set; }
        bool IsSpeaking();
        bool IsWaitingUserInput();
        void Say(ISpeech speech);
        void Skip();
        void StopSpeaking();
        void Close();
    }
}