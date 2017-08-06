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
    public AudioClip Death;

	public Transform carryPosition;
	public Transform dropPosition;
	public Transform throwPosition;
	public float throwDuration;
	public float throwTime;
	public bool beenThrown = false;
	public bool carrying = false;
	public bool beingCarried = false;
	public float throwForce;

	public bool facingRight = true;

	public Transform spawnPoint;
	public bool youDed = false;
  public AudioSource source;
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
      //source = GetComponent<AudioSource>();
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

		/*
		if(beenThrown)
		{
			BeingThrown();
		}
		*/

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

	public void Throw()
	{
		if(carryObjectController != null)
		{
			if(carryObjectController.transform.parent != null)
			{
				carryObjectController.setThisParent(null);
			}
		}


		carryObjectController.beingCarried = false;
		carryObject.GetComponent<BoxCollider2D>().enabled = true;
		carryObjectRB.isKinematic = false;
		Invoke("setCarrying", .1f);


		Vector2 throwDirection = (throwPosition.position - transform.position).normalized;

		carryObjectRB.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

		carryObjectController.beenThrown = true;
		//canCarry = false;
		carryObject = null;
		carryObjectRB = null;
		carryObjectController = null;
	}

	/*
	public void BeingThrown()
	{
		Vector2 throwDirection = (this.throwPosition.position - this.transform.position).normalized;


		if(throwDuration > throwTime)
		{
			beenThrown = false;
			return;
		}

		else
		{
			throwDuration += Time.deltaTime;

			carryObjectRB.AddForce(throwDirection * throwForce);
		}
	}
	*/

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
	public void SetDropValues(){

		carryObject = null;
	}

	public void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void ded()
	{
		youDed = false;
		transform.position = spawnPoint.position;
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		if(collider.gameObject.tag == "Kill")
		{
			youDed = true;
			Invoke("ded",3);
      source.PlayOneShot(Death);
		}
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
