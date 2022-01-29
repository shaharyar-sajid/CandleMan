using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,4);
		
		//transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "lethal")
		{
			Destroy(gameObject);
		}
	}
	
	public void Move(Vector3 moveDirection)
	{
		targetPos += moveDirection;
	}
}
