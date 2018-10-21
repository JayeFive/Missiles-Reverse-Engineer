using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.UIExtensions
{
    public static class GraphicExtensions
    {
        // Fade methods for UI elements


        public static IEnumerator CrossFadeAlphaFixed(this Graphic elem, float alpha, float duration = 1.0f)
        {
            var startTime = Time.time;
            var timeSinceStart = Time.time - startTime;
            var lerpPercentage = timeSinceStart / duration;

            Color currentColor = elem.color;
            Color visibleColor = elem.color;
            visibleColor.a = alpha;

            while (lerpPercentage <= 1f)
            {
                timeSinceStart = Time.time - startTime;
                lerpPercentage = timeSinceStart / duration;
                elem.color = Color.Lerp(currentColor, visibleColor, lerpPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
