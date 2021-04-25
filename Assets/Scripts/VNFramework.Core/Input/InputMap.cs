using System.Collections.Generic;
using System.Linq;
using VNFramework.Interfaces.Input;

namespace VNFramework.Core.Input
{
    public class InputMap : Dictionary<string, IInputAction>, IInputMap
    {
        public string Name { get; protected set; }
        public bool Enabled { get; set; }

        public InputMap(string name, bool enabled = true)
        {
            Name = name;
            Enabled = enabled;
        }

        public void AddAction(IInputAction action)
        {
            Add(action.Name, action);
        }

        public void AddActions(params IInputAction[] actions)
        {
            foreach (IInputAction action in actions) { AddAction(action); }
        }

        public IInputAction GetAction(string actionName)
        {
            return this[actionName];
        }

        public new IEnumerator<IInputAction> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public bool ContainsAction(string actionName)
        {
            return ContainsKey(actionName);
        }

        public void Update()
        {
            if (!Enabled) { return; }

            foreach (IInputAction action in Values.Where(i => i.Enabled)) { action.Update(); }
        } 
    }
}
