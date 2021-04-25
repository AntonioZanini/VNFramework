using System.Collections.Generic;

namespace VNFramework.Interfaces.Input
{
    public interface IInputMap : IEnumerable<IInputAction>
    {
        string Name { get; }
        bool Enabled { get; set; }
        IInputAction GetAction(string actionName);
        void AddAction(IInputAction action);
        void AddActions(params IInputAction[] actions);
        bool ContainsAction(string actionName);
        void Update();
    }
}
