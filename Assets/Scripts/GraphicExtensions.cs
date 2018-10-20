using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.UIExtensions
{
    public static class GraphicExtensions
    {
        // Fade methods for UI elements

        //public static void FadeIn(this Graphic g)
        //{
        //    g.canvasRenderer.SetAlpha(0.0f);
        //    g.CrossFadeAlpha(1.0f, 3.0f, false);
        //}

        //public static void FadeOut(this Graphic g)
        //{
        //    g.canvasRenderer.SetAlpha(1.0f);
        //    g.CrossFadeAlpha(0.0f, 3.0f, false);
        //}

        public static IEnumerator CrossFadeAlphaFixed(this Graphic elem, float alpha, float duration)
        {
            Color currentColor = elem.color;

            Color visibleColor = elem.color;
            visibleColor.a = alpha;

            float counter = 0;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                elem.color = Color.Lerp(currentColor, visibleColor, counter / duration);
                yield return null;
            }
        }
    }
}
