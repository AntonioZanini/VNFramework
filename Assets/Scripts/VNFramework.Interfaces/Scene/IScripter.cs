using System;
using System.Collections.Generic;
using System.Text;

namespace VNFramework.Interfaces.Scene
{
    public interface IScripter
    {
        void Initialize();
        void ExecuteNext();
    }
}
