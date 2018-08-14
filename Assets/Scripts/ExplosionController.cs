using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    [SerializeField] GameObject circle;
    [SerializeField] float innerCircleSize;
    [SerializeField] float outerCircleSize;
    [SerializeField] float cloudGrowSpeed;
    [SerializeField] float cloudFadeSpeed;
    [SerializeField] float cloudDissipateSpeed;
    [SerializeField] float explosionFadeSpeed;
    [SerializeField] float explosionLifetime;
    [SerializeField] int numSmokeCircles;
    [SerializeField] int numExplosionCircles;
    [SerializeField] float smokeCircleRange;
    [SerializeField] float explosionCircleRange;
    [SerializeField] float cloudDriftSpeed;
    [SerializeField] float cloudDriftSpeedModifier;


    public void MissleToMissle()
    {
        BeginAnimation(numSmokeCircles, smokeCircleRange, false);
        BeginAnimation(numExplosionCircles, explosionCircleRange, true);
    }

    private void BeginAnimation(int numCircles, float range, bool isExplosion)
    {
        for (int i = 0; i < numCircles; i++)
        {
            var newCircle = SpawnCircle(range, isExplosion);
            StartCoroutine(CloudGrow(newCircle, isExplosion ? innerCircleSize : outerCircleSize));
        }
    }

    public void MissleToAirplane()
    {

    }

    private GameObject SpawnCircle(float range, bool isExplosion)
    {
        GameObject newCircle = Instantiate(circle, transform);
        newCircle.transform.position += (CircleOffset(range) + new Vector3(0, 0, 0.75f));

        if (isExplosion)
        {
            newCircle.GetComponent<circle>().isExplosion = true;
            newCircle.GetComponent<SpriteRenderer>().color = GetExplosionColor();
            newCircle.transform.position -= new Vector3(0, 0, 0.25f);
        }
        else
        {
            newCircle.GetComponent<circle>().cloudDrift = new Vector3(GetCloudDriftSpeed(), 0, 0);
        }

        return newCircle;
    }

    private IEnumerator CloudGrow(GameObject newCircle, float circleSize)
    {
        for (float scale = 0; scale <= circleSize; scale += cloudGrowSpeed)
        {
            newCircle.transform.localScale = new Vector3(scale, scale, 0);
            yield return 0;
        }


        if (newCircle.GetComponent<circle>().isExplosion)
        {
            StartCoroutine(ExplosionLife(newCircle, circleSize));
        }
        else
        {
            StartCoroutine(CloudFade(newCircle));
        }
    }

    private IEnumerator ExplosionDissipate(GameObject newCircle, float circleSize)
    {

        for (float scale = circleSize; scale >= 0; scale -= explosionFadeSpeed)
        {
            newCircle.transform.localScale = new Vector3(scale, scale, 0);
            yield return null;
        }

        Destroy(newCircle);
    }

    private IEnumerator CloudFade(GameObject newCircle)
    {
        int finalOpactity = GetRandomOpacity();
        Color32 color = newCircle.GetComponent<SpriteRenderer>().color;

        for (float opacity = 255; opacity >= finalOpactity; opacity -= cloudFadeSpeed)
        {
            color.a = (byte)opacity;
            newCircle.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        StartCoroutine(CloudDissipate(newCircle));
    }

    private IEnumerator CloudDissipate (GameObject newCircle)
    {
        Color32 color = newCircle.GetComponent<SpriteRenderer>().color;
        float startingOpacity = color.a;

        for (float opacity = startingOpacity; opacity >= 0; opacity -= cloudDissipateSpeed)
        {
            color.a = (byte)opacity;
            newCircle.GetComponent<SpriteRenderer>().color = color;
            yield return 0;
        }

        Destroy(newCircle);
        StartCoroutine(CheckForEnd());
    }

    private IEnumerator CheckForEnd()
    {
        yield return new WaitForEndOfFrame();

        if (gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private Vector3 CircleOffset(float range)
    {
        return UnityEngine.Random.insideUnitCircle * range;
    }

    private Color32 GetExplosionColor()
    {
        var r = (byte)UnityEngine.Random.Range(200, 255);
        var g = (byte)UnityEngine.Random.Range(0, 60);
        var b = (byte)UnityEngine.Random.Range(0, 60);

        return new Color32(r, g, b, 255);
    }

    private int GetRandomOpacity()
    {
        return UnityEngine.Random.Range(35, 55);
    }

    private IEnumerator ExplosionLife (GameObject newCircle, float circleSize)
    {
        yield return new WaitForSeconds(explosionLifetime);

        StartCoroutine(ExplosionDissipate(newCircle, circleSize));
    }

    private float GetCloudDriftSpeed ()
    {
        return UnityEngine.Random.Range(cloudDriftSpeed - cloudDriftSpeedModifier, cloudDriftSpeed + cloudDriftSpeedModifier);
    }
}