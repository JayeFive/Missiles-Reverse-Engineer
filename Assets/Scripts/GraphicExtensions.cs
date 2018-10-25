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

            var currentColor = elem.color;
            var visibleColor = elem.color;
            visibleColor.a = alpha;

            while (lerpPercentage <= 1f)
            {
                timeSinceStart = Time.time - startTime;
                lerpPercentage = timeSinceStart / duration;
                elem.color = Color.Lerp(currentColor, visibleColor, lerpPercentage);
                yield return new WaitForEndOfFrame();
            }
        }

        public static IEnumerator FadeToSize(this Graphic elem, float multiplier, float duration = 1.0f)
        {
            var startTime = Time.time;
            var timeSinceStart = Time.time - startTime;
            var lerpPercentage = timeSinceStart / duration;

            var currentScale = elem.rectTransform.localScale;
            var visibleScale = currentScale * multiplier;

            while (lerpPercentage <= 1f)
            {
                timeSinceStart = Time.time - startTime;
                lerpPercentage = timeSinceStart / duration;
                elem.rectTransform.localScale = Vector3.Lerp(currentScale, visibleScale, lerpPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
