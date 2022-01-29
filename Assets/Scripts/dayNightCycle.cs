using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dayNightCycle : MonoBehaviour
{
	[Range(0.0f, 1.0f)]
	public float time;
	public float fullDayLength;
	public float startTime = 0.4f;
	private float timeRate;
	public Vector3 noon;
	// Start is called before the first frame update
	static public bool blackout = false;
	
	[Header("Sun")]
	public Light sun;
	public Gradient sunColor;
	public AnimationCurve sunIntensity;
	
	[Header("Moon")]
	public Light moon;
	public Gradient moonColor;
	public AnimationCurve moonIntensity;
	
	[Header("Other Lighting")]
	public AnimationCurve lightingIntensityMultiplier;
	public AnimationCurve reflectionsIntensityMultiplier;
	
    void Start()
    {
		blackout = false;
        timeRate = 1.0f/fullDayLength;
		time = startTime;
		//sun.transform.Rotate(45f,45.0f,45f);
    }

    // Update is called once per frame
    void Update()
    {
		//increment time
        time += timeRate * Time.deltaTime;
		
		if(time >= 1.0f)
		{
			time = 0.0f;
		}
		
		//light rotation
		sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
		moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;
		//sun.transform.Rotate(0,45.0f,0);
		
		//light intensity
		sun.intensity = sunIntensity.Evaluate(time);
		moon.intensity = moonIntensity.Evaluate(time);
		
		//change colors
		sun.color = sunColor.Evaluate(time);
		moon.color = moonColor.Evaluate(time);
		
		//enable / disable sun
		if(sun.intensity == 0 && sun.gameObject.activeInHierarchy)
		{
			print("Sun down");
			sun.gameObject.SetActive(false);
			blackout = true;

		}
		else if(sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
		{
			print("Sun up");
			sun.gameObject.SetActive(true);
			blackout = false;
		}
		
		//enable / disable moon
		if(moon.intensity == 0 && moon.gameObject.activeInHierarchy)
		{
			moon.gameObject.SetActive(false);
		}
		else if(moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
		{
			moon.gameObject.SetActive(true);
		}
		
		//lighting and reflections intensity
		RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
		RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
    }
}
