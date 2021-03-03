using UnityEngine.InputSystem;
using VNFramework.Interfaces.Input;

namespace Assets.Scripts.VNFramework.Core.Input
{
    public class DataInputAction : IDataInputAction
    {
        public string name { get; set; }
        public InputActionType type { get; set; }
        public string binding { get; set; }
        public string interactions { get; set; }
        public string processors { get; set; }
        public string groups { get; set; }
        public string expectedControlLayout { get; set; }

        public DataInputAction()
        {
            type = InputActionType.Value;
            binding = null;
            interactions = null;
            processors = null;
            groups = null;
            expectedControlLayout = null;
        }

        public DataInputAction(string actionName, 
                               InputActionType actionType = InputActionType.Value, 
                               string actionBinding = null)
        {
            name = actionName;
            type = actionType;
            binding = actionBinding;
            interactions = null;
            processors = null;
            groups = null;
            expectedControlLayout = null;
        }
    }
}
