using UnityEngine;

namespace VNFramework.Core.Helpers
{
    public static class GraphicHelpers
    {
        public static Color SetAlpha(Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }

}
