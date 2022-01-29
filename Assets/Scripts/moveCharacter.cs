using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCharacter : MonoBehaviour
{
	public KeyCode moveL;
	public KeyCode moveR;
	
	public Transform dropPiece, dropPuddle;
	
	public float horizVel = 0.0f;
	public int laneNum = 2;
	public bool controlLock = false;
	public float height;
	private Vector3 scaleChange;
	private bool increase = false, decrease = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, GM.verticalVel, 4);
		
		if(increase || decrease)
		{
			height = 0.05f;
			
			if(increase)
			{
				scaleChange = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y + height, gameObject.transform.localScale.z);
				gameObject.transform.localScale = scaleChange;
				increase = false;
			}
			else
			{
				scaleChange = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - height, gameObject.transform.localScale.z);
				gameObject.transform.localScale = scaleChange;
				decrease = false;
			}
		}
		
		if ((Input.GetKeyDown(moveL)) && (laneNum > 1) && (!controlLock))
		{
			horizVel = -2.0f;
			StartCoroutine(stopSlide());
			laneNum -= 1;
			controlLock = true;
		}
		if ((Input.GetKeyDown(moveR)) && (laneNum < 3) && (!controlLock))
		{
			horizVel = 2.0f;
			StartCoroutine(stopSlide());
			laneNum += 1;
			controlLock = true;
		}
    }
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "lethal")
		{
			Destroy(gameObject);
		}
		if(other.gameObject.tag == "powerUp")
		{
			Destroy(other.gameObject);
			increase = true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "powerDown")
		{
			Debug.Log(other.gameObject.name);
			decrease = true;
			if(other.gameObject.name == "Cylinder.001")
			{
				Instantiate(dropPiece, gameObject.transform.position, dropPiece.rotation);
			}
			else if(other.gameObject.name == "Woods Variant 1(Clone)")
			{
				Instantiate(dropPuddle, gameObject.transform.position, dropPuddle.rotation);
				Debug.Log("Entered");
			}
		}
		if(other.gameObject.name == "rampTrig")
		{
			GM.verticalVel = 2;
		}
		if(other.gameObject.name == "rampTrig2")
		{
			GM.verticalVel = 0;
		}
	}
	
	IEnumerator stopSlide()
	{
		yield return new WaitForSeconds(.5f);
		horizVel = 0.0f;
		controlLock = false;
	}
}
