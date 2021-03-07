using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene
{
    public class ScriptText : IScriptText
    {
        public string GetText()
        {
            string lineBreak = System.Environment.NewLine;
            return $"TESTE {lineBreak} " +
                   $"TESTE {lineBreak} " +
                   $"TESTE {lineBreak} ";
        }
    }
}
