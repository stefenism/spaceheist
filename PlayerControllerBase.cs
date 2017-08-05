using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBase : MonoBehaviour {

	[HideInInspector]
	public Rigidbody2D rb;
	public bool grounded = true;


	//[HideInInspector]
	public bool canCarry = false;
	public GameObject carryObject;
	public PlayerControllerBase carryObjectController;
	public Rigidbody2D carryObjectRB;

	public Transform carryPosition;
	public Transform dropPosition;
	public bool carrying = false;
	public bool beingCarried = false;
/*
	[HideInInspector]
	[Header("Run Stuff")]
	public float moveDir;
	public bool canRun = true;
	public float speed;

	[Header("Jump Stuff")]
	public bool jumping;
	public bool canJump;
	public bool jumpAllowed;
	public float jumpForce;
	public float jumpTime;
	public float jumpDuration;
	*/

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if(canCarry)
		{
			Carry();
		}

		if(carrying)
		{
			Drop();
		}

		Debug.Log("cancarry: " + canCarry);
	}

	public void Carry()
	{
		//Rigidbody2D carryObjectRB = carryObject.GetComponent<Rigidbody2D>();

		//carrying = true;
		canCarry = false;
		Debug.Log("carryObject: " + carryObject.name);
		//carryObject.transform.parent = this.gameObject.transform;
		//carryObject.transform.SetParent(this.gameObject.transform);
		carryObjectController.setThisParent(this.gameObject.transform);
		carryObject.transform.position = carryPosition.position;
		carryObjectController.beingCarried = true;
		carryObject.GetComponent<BoxCollider2D>().enabled = false;//("false");
		carryObjectRB.isKinematic = true;
		Invoke("setCarrying", .1f);

	}

	public void Drop()
	{
		//Rigidbody2D carryObjectRB = carryObject.GetComponent<Rigidbody2D>();



		//carryObject.transform.parent = null;
		//carryObject.transform.SetParent(null);
		carryObjectController.setThisParent(null);
		carryObject.transform.position = dropPosition.position;
		carryObjectController.beingCarried = false;
		carryObject.GetComponent<BoxCollider2D>().enabled = true;
		carryObjectRB.isKinematic = false;
		Invoke("setCarrying", .1f);

		//canCarry = false;
		carryObject = null;
		carryObjectRB = null;
		carryObjectController = null;
	}

	void setCarrying()
	{
		carrying = !carrying;
	}

	public void setThisParent(Transform parentt)
	{


		if(parentt == null)
		{
			this.transform.parent = null;
		}
		else
			this.transform.parent = parentt;
	}

	public void SetPickupValues(GameObject collided){

		canCarry = true;
		carryObject = collided.gameObject;
		carryObjectController = carryObject.GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;
		carryObjectRB = carryObject.GetComponent<Rigidbody2D>();
	}
	public void SetDropValues(GameObject collided){

		carryObject = null;
	}

	/*
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			canCarry = true;
			carryObject = collider.gameObject;
			carryObjectController = GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;
			carryObjectRB = carryObject.GetComponent<Rigidbody2D>();
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		canCarry = false;
		carryObject = null;
		//carryObjectRB = null;
	}
	*/
}
