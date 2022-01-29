using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleLight : MonoBehaviour
{
    public float fadeFrom = 10f;
    public float fadeTo = 1f;

    public AnimationCurve lerpCuve;
    public float lerpTime = 3f;
    private float _timer = 0f;

    Light _light;
    bool trigger;
    bool trigger2;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
        _light = GetComponent<Light>();
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dayNightCycle.blackout == true)
        {
            if(_timer < lerpTime)
            {
                _timer += Time.deltaTime;
            }

            float lerpRatio = _timer / lerpTime;

            _light.range = Mathf.Lerp(fadeFrom, fadeTo, lerpRatio);

            trigger = true;
        }
        if (dayNightCycle.blackout == false && trigger == true && trigger2 == false)
        {
            _light.range = 10.0f;
            trigger2 = true;
        }
    }
    /*void Fade(Light l, float fadeStart, float fadeEnd, float fadeTime)
    {
        float t = 0.0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            l.range = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
            //yield return new WaitForSeconds(1.0f);
        }
    }*/

    IEnumerator Fade(Light l, float fadeStart, float fadeEnd, int fadeTime)
    {
        int t = 0;

        while (t < fadeTime)
        {
            l.range += (fadeEnd - fadeStart / fadeTime);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Fade1(float seconds)
    {

        // loop over 1 second backwards
        for (float i = seconds; i >= 0.0; i -= Time.deltaTime)
        {
            //_light.range = (10.0-1.0)
            yield return null;
        }
       
    }
} 
