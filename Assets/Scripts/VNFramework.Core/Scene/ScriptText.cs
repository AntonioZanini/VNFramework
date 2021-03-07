using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene
{
    public class ScriptText : IScriptText
    {
        public string GetText()
        {
            return ResourceHelpers.LoadScriptText("Scene01");
        }
    }
}
