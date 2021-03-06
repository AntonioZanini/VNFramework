﻿using System.Collections.Generic;

namespace VNFramework.Interfaces.Input
{
    public interface IInputManager
    {
        void Start();
        void Stop();
        void AddMap(string mapName);
        void AddMap(IEnumerable<string> mapNames);
        void AddAction(string mapName, IInputAction action);
        void AddActions(string mapName, IEnumerable<IInputAction> actions);
        void Register(string mapName, IInputHandler handler);
        void Register(string mapName, string actionName, IInputHandler handler);
        void Unregister(string mapName, IInputHandler handler);
        void Unregister(string mapName, string actionName, IInputHandler handler);
    }
}
