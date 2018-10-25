using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Assets.UIExtensions;

public class WorldSpaceCanvas : MonoBehaviour
{
    [SerializeField] private float bonusFadeSpeed = 0.0f;
    private Text bonusText;

    //MonoBehavior
    void Awake ()
    {
        bonusText = GetComponentInChildren<Text>();
    }

    void Update ()
    {
        if(bonusText != null && bonusText.color.a == 0)
        {
            Destroy(gameObject);
        }
    }

    public void BonusAnimation (int bonusValue)
    {
        Color bonusColor = bonusText.color;
        bonusColor.a = 0.01f;
        bonusText.color = bonusColor;

        bonusText.text = "+" + bonusValue.ToString();
        StartCoroutine(ShowBonus(bonusText));
    }

    private IEnumerator ShowBonus (Text text)
    {
        StartCoroutine(text.CrossFadeAlphaFixed(1f, bonusFadeSpeed));

        yield return new WaitForSeconds(3f);

        StartCoroutine(text.CrossFadeAlphaFixed(0f, bonusFadeSpeed));
    }
}
