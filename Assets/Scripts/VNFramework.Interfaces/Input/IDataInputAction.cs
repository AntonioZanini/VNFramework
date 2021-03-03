using UnityEngine.InputSystem;

namespace VNFramework.Interfaces.Input
{
    public interface IDataInputAction
    {
        string name { get; set; }
        InputActionType type { get; set; }
        string binding { get; set; }
        string interactions { get; set; }
        string processors { get; set; }
        string groups { get; set; }
        string expectedControlLayout { get; set; }
    }
}
