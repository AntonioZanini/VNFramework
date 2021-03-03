using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using VNFramework.Interfaces.Input;

namespace Assets.Scripts.VNFramework.Core.Input
{
    public class InputManager : IInputManager
    {
        protected enum InputEventHandleAction
        {
            Add,
            Remove
        }

        protected enum InputEventType
        {
            performed,
            canceled,
            started
        }

        protected Dictionary<string, InputActionMap> actionMaps = new Dictionary<string, InputActionMap>();

        public void Enable(string mapName)
        {
            GetMap(mapName).Enable();
        }

        public void Disable(string mapName)
        {
            GetMap(mapName).Disable();
        }

        public void AddMap(string mapName)
        {
            if (!actionMaps.ContainsKey(mapName))
            {
                var map = new InputActionMap(mapName);
                actionMaps.Add(mapName, map);
                ScriptableObject.CreateInstance<InputActionAsset>().AddActionMap(map);
            }
        }
        
        public void AddMap(IEnumerable<string> mapNames)
        {
            foreach (string mapName in mapNames) { AddMap(mapName); }
        }

        public void AddAction(string mapName, IDataInputAction action)
        {
            
            AddAction(GetMap(mapName), action);
        }

        public void AddActions(string mapName, IEnumerable<IDataInputAction> actions)
        {
            InputActionMap map = GetMap(mapName);
            foreach (IDataInputAction action in actions) { AddAction(map, action); }
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

        protected InputActionMap GetMap(string mapName)
        {
            if (mapName.Trim().Equals(string.Empty)) { throw new KeyNotFoundException(); }
            if (!actionMaps.ContainsKey(mapName)) { throw new KeyNotFoundException(); }
            return actionMaps[mapName];
        }

        protected void AddAction(InputActionMap map, IDataInputAction action)
        {
            map.AddAction(action.name,
                          action.type,
                          action.binding,
                          action.interactions,
                          action.processors,
                          action.groups,
                          action.expectedControlLayout);
        }

        protected void SetEvent(string mapName, IInputHandler handler, InputEventHandleAction handleAction)
        {
            if (actionMaps.ContainsKey(mapName))
            {
                foreach (InputAction action in actionMaps[mapName])
                {
                    SetEvent(action, InputEventType.performed, handleAction, handler);
                }
            }
        }

        protected void SetEvent(string mapName, string actionName, IInputHandler handler, InputEventHandleAction handleAction)
        {
            if (actionMaps.ContainsKey(mapName) && actionMaps[mapName].Any(a => a.name.Equals(actionName)))
            {
                SetEvent(actionMaps[mapName][actionName], InputEventType.performed, handleAction, handler);
            }
        }

        protected void SetEvent(InputAction action, InputEventType eventType, InputEventHandleAction handleAction, IInputHandler handler)
        {
            switch (eventType)
            {
                case InputEventType.performed:
                    if (handleAction == InputEventHandleAction.Add)
                        action.performed += handler.HandleInput;
                    else
                        action.performed -= handler.HandleInput;
                    break;
                case InputEventType.canceled:
                    if (handleAction == InputEventHandleAction.Add)
                        action.canceled += handler.HandleInput;
                    else
                        action.canceled -= handler.HandleInput;
                    break;
                case InputEventType.started:
                    if (handleAction == InputEventHandleAction.Add)
                        action.started += handler.HandleInput;
                    else
                        action.started -= handler.HandleInput;
                    break;
            }
            
        }

    }
}
