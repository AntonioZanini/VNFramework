using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VNFramework.Core.Settings;
using VNFramework.Interfaces;
using VNFramework.Interfaces.Global;
using VNFramework.Interfaces.Input;

namespace VNFramework.Core.Input
{
    public class InputManager : IInputManager
    {
        protected enum InputEventHandleAction
        {
            Add,
            Remove
        }

        protected Dictionary<string, IInputMap> actionMaps = new Dictionary<string, IInputMap>();

        protected Coroutine update = null;

        protected ICoroutineAccessor coroutineAccessor;

        public InputManager()
        {
            coroutineAccessor = Configurations.GlobalConfiguration.CoroutineAccessor;
        }

        public bool Active { get; protected set; }

        public void AddMap(string mapName)
        {
            if (!actionMaps.ContainsKey(mapName))
            {
                IInputMap map = new InputMap(mapName);
                actionMaps.Add(mapName, map);
            }
        }

        public void AddMap(IEnumerable<string> mapNames)
        {
            foreach (string mapName in mapNames) { AddMap(mapName); }
        }

        public void AddAction(string mapName, IInputAction action)
        {
            AddAction(GetMap(mapName), action);
        }

        public void AddActions(string mapName, IEnumerable<IInputAction> actions)
        {
            IInputMap map = GetMap(mapName);
            foreach (IInputAction action in actions) { AddAction(map, action); }
        }

        public void Register(string mapName, IInputHandler handler)
        {
            SetEvent(mapName, handler, InputEventHandleAction.Add);
        }

        public void Register(string mapName, string actionName, IInputHandler handler)
        {
            SetEvent(mapName, actionName, handler, InputEventHandleAction.Add);
        }

        public void Unregister(string mapName, IInputHandler handler)
        {
            SetEvent(mapName, handler, InputEventHandleAction.Remove);
        }

        public void Unregister(string mapName, string actionName, IInputHandler handler)
        {
            SetEvent(mapName, actionName, handler, InputEventHandleAction.Remove);
        }

        protected IInputMap GetMap(string mapName)
        {
            if (mapName.Trim().Equals(string.Empty)) { throw new KeyNotFoundException(); }
            if (!actionMaps.ContainsKey(mapName)) { throw new KeyNotFoundException(); }
            return actionMaps[mapName];
        }

        protected void AddAction(IInputMap map, IInputAction action)
        {
            map.AddAction(action);
        }

        protected void SetEvent(string mapName, IInputHandler handler, InputEventHandleAction handleAction)
        {
            if (actionMaps.ContainsKey(mapName))
            {
                foreach (IInputAction action in actionMaps[mapName])
                {
                    SetEvent(action, InputEventType.KeyUp, handleAction, handler);
                }
            }
        }

        protected void SetEvent(string mapName, string actionName, IInputHandler handler, InputEventHandleAction handleAction)
        {
            if (actionMaps.ContainsKey(mapName) && actionMaps[mapName].ContainsAction(actionName))
            {
                SetEvent(actionMaps[mapName].GetAction(actionName), InputEventType.KeyUp, handleAction, handler);
            }
        }

        protected void SetEvent(IInputAction action, InputEventType eventType, InputEventHandleAction handleAction, IInputHandler handler)
        {
            switch (eventType)
            {
                case InputEventType.KeyUp:
                    if (handleAction == InputEventHandleAction.Add)
                        action.KeyUp += handler.HandleInput;
                    else
                        action.KeyUp -= handler.HandleInput;
                    break;
                case InputEventType.KeyDown:
                    if (handleAction == InputEventHandleAction.Add)
                        action.KeyDown += handler.HandleInput;
                    else
                        action.KeyDown -= handler.HandleInput;
                    break;
            }
        }

        public void Start()
        {
            if (Active) { return; }
            Active = true;
            update = coroutineAccessor.StartCoroutine(Update());
        }

        public void Stop()
        {
            if (!Active) { return; }
            Active = false;
        }

        IEnumerator Update()
        {
            while (Active)
            {
                foreach (IInputMap map in actionMaps.Values.Where(m => m.Enabled)) { map.Update(); }
                yield return new WaitForEndOfFrame();
            }
            update = null;
        }


    }
}
