using System.Collections;
using UnityEngine;
using VNFramework.Core.Settings;

namespace VNFramework.Core.Helpers
{
    public static class BaseHelpers
    {
        public static Coroutine StartCoroutine(IEnumerator coroutineFunction)
        {
            return Configurations.GlobalConfiguration.GameObject.StartCoroutine(coroutineFunction);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            Configurations.GlobalConfiguration.GameObject.StopCoroutine(coroutine);
        }

    }
}
