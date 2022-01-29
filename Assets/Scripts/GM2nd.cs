using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GM2nd : MonoBehaviour
{
	public float verticalVel;
	private Vector2 startPos;
	public int pixelDistToDetect = 20;
	private bool fingerDown, mouseDown;
	public float horizVel = 0.0f;
	public int laneNum = 2;
	private float moveFactor = 0.005f;
	public bool controlLock = false;
	public float height;
	private Vector3 scaleChange;
	private bool increase = false, decrease = false;
	private Animator anim;
	public static bool isDead;
	public static bool isLevelComplete;
	public Image restartButton;
	public Transform dropPiece, dropPuddle;
	AudioSource[] audioData;
	static public AudioListener audioListener;
	static public AudioSource audioRunning, audioMusic;
	AudioSource audioCut, audioDeath, audioBurn, audioPickup, audioCelebration;
	public GameObject levelFinishPanel;
    // Start is called before the first frame update

    void Start()
	{
		
		audioData = GetComponents<AudioSource>();
		audioListener = GetComponent<AudioListener>();
		audioCut = audioData[0];
		audioRunning = audioData[1];
		audioDeath = audioData[2];
		audioBurn = audioData[3];
		audioPickup = audioData[4];
		audioCelebration = audioData[5];
		audioMusic = audioData[6];

		anim = GetComponent<Animator>();
		isDead = false;
		isLevelComplete = false;
		restartButton.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDead && !isLevelComplete)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, 4);
			if (increase || decrease)
			{
				height = 0.05f;

				if (increase)
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

			if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
			{
				startPos = Input.touches[0].position;
				fingerDown = true;
			}

			if (fingerDown)
			{
				if (Input.touches[0].position.x < startPos.x || Input.touches[0].position.x > startPos.x)
				{
					float diff = Input.touches[0].position.x - startPos.x;
					if ((transform.position.x + diff * moveFactor) < -1.0f)
					{
						transform.position = new Vector3(-1.0f, transform.position.y, transform.position.z);
					}
					else if ((transform.position.x + diff * moveFactor) > 1.0f)
					{
						transform.position = new Vector3(1.0f, transform.position.y, transform.position.z);
					}
					else
					{
						transform.position += new Vector3(diff * moveFactor, 0, 0);
					}
					startPos = Input.touches[0].position;
				}
				//if(Input.touches[0].position.x <= startPos.x - pixelDistToDetect && (laneNum > 1) && (!controlLock))
				//{
				//	horizVel = -2.0f;
				//	StartCoroutine(stopSlide());
				//	laneNum -= 1;
				//	controlLock = true;

				//	fingerDown = false;
				//	print("Swipe Left");
				//}
				//else if(Input.touches[0].position.x >= startPos.x + pixelDistToDetect && (laneNum < 3) && (!controlLock))
				//{
				//	horizVel = 2.0f;
				//	StartCoroutine(stopSlide());
				//	laneNum += 1;
				//	controlLock = true;

				//	fingerDown = false;
				//	print("Swipe Right");
				//}
			}

			if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
			{
				fingerDown = false;
			}

			//Testing for PC
			if (mouseDown == false && Input.GetMouseButtonDown(0))
			{
				startPos = Input.mousePosition;
				mouseDown = true;
			}
			if (mouseDown)
			{
				if (Input.mousePosition.x < startPos.x || Input.mousePosition.x > startPos.x)
				{
					float diff = Input.mousePosition.x - startPos.x;
					if ((transform.position.x + diff * moveFactor) < -1.0f)
					{
						transform.position = new Vector3(-1.0f, transform.position.y, transform.position.z);
					}
					else if ((transform.position.x + diff * moveFactor) > 1.0f)
					{
						transform.position = new Vector3(1.0f, transform.position.y, transform.position.z);
					}
					else
					{
						transform.position += new Vector3(diff * moveFactor, 0, 0);
					}
					//print("Position" + transform.position.x.ToString());
					startPos = Input.mousePosition;
				}
				//if(Input.mousePosition.x <= startPos.x - pixelDistToDetect && (laneNum > 1) && (!controlLock))
				//{
				//	horizVel = -2.0f;
				//	StartCoroutine(stopSlide());
				//	laneNum -= 1;
				//	controlLock = true;

				//	mouseDown = false;
				//	print("Swipe Left");
				//}
				//else if(Input.mousePosition.x >= startPos.x + pixelDistToDetect && (laneNum < 3) && (!controlLock))
				//{
				//	horizVel = 2.0f;
				//	StartCoroutine(stopSlide());
				//	laneNum += 1;
				//	controlLock = true;

				//	mouseDown = false;
				//	print("Swipe Right");
				//}

			}
			if (mouseDown && Input.GetMouseButtonUp(0))
			{
				mouseDown = false;
			}
		}

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "lethal")
		{
			KillPlayer();
		}
		if (other.gameObject.tag == "powerUp")
		{
			Destroy(other.gameObject);
			if (gameObject.transform.localScale.y < 0.8f)
			{
				increase = true;
			}
			score._score += 5000;
			audioPickup.Play();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "powerDown")
		{
			if (other.gameObject.name == "Cylinder.001")
			{
				Instantiate(dropPiece, gameObject.transform.position, dropPiece.rotation);
				audioCut.Play();
			}
			else if(other.gameObject.name == "Woods Variant 1(Clone)")
			{
				Instantiate(dropPuddle, gameObject.transform.position, dropPuddle.rotation);
				audioBurn.Play();
			}
			if (gameObject.transform.localScale.y <= 0.2f)
			{
				KillPlayer();
				
			}
			else
			{
				decrease = true;
				score._score -= 3000;
			}
		}
		if (other.gameObject.name == "rampTrig")
		{
			verticalVel = 2;
		}
		if (other.gameObject.name == "rampTrig2")
		{
			verticalVel = 0;
		}
		if (other.gameObject.tag == "Finish")
		{
			StartCoroutine(finishAnimation());
		}
		if (other.gameObject.tag == "cake")
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}

	}

	IEnumerator stopSlide()
	{
		yield return new WaitForSeconds(.5f);
		horizVel = 0.0f;
		controlLock = false;
	}
	IEnumerator finishAnimation()
	{
		audioMusic.volume = 0.04f;
		score._scoreUpdate = false;
		transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
		progressBar.rt.sizeDelta = new Vector2(100f, 100f);
		isLevelComplete = true;
		GetComponent<Rigidbody>().drag = 0.3f;
		yield return new WaitForSeconds(3.5f);
		anim.SetBool("isLevelComplete", true);
		audioRunning.Stop();
		yield return new WaitForSeconds(2f);
		audioCelebration.Play();
		yield return new WaitForSeconds(1f);
		levelFinishPanel.SetActive(true);
		PlayerPrefs.SetInt("score", score._score);
		PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1)+1);
	}

	void KillPlayer()
	{
		audioRunning.Stop();
		audioMusic.Stop();
		audioDeath.Play();
		score._scoreUpdate = false;
		score._score = 0;
		isDead = true;
		anim.SetBool("isDead", true);
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		GetComponent<Rigidbody>().drag = 10f;
		restartButton.enabled = true;
		PlayerPrefs.SetInt("score", score._score);
		PlayerPrefs.SetInt("level", 1);
	}
}
