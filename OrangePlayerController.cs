using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayerController : PlayerControllerBase {


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


	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

		CheckInput();

		Physics2D.IgnoreLayerCollision(9,10);
	}

	void FixedUpdate()
	{
		if(canRun)
		{
			rb.velocity = new Vector2(speed * moveDir, rb.velocity.y);//.right * speed * moveDir;
		}

		if(jumpAllowed)
		{
			Jump();
		}

		if(canCarry)
		{
			if(Input.GetButtonDown("Pickup"))
			{
				Carry();
			}
		}

		if(carrying)
		{
			if(Input.GetButtonDown("Pickup") && !beingCarried)
			{
				Drop();
			}

		}
	}

	void CheckInput()
	{
		moveDir = Input.GetAxis("Horizontal");

		if(grounded)
		{
			jumping = false;
			jumpAllowed = true;
			jumpDuration = 0;
			canJump = true;
			canRun = true;
		}

		else
		{
			canJump = false;
		}

		JumpButton();
	}

	void JumpButton()
	{
		if(Input.GetButton("Jump"))
		{
			jumping = true;
			canJump = false;
		}
		else
		{
			jumping = false;
		}

		if(Input.GetButtonUp("Jump"))
		{
			jumpAllowed = false;
		}

		if(jumpDuration >= jumpTime)
	{
		jumping = false;
		jumpAllowed = false;
	}

	if(!grounded && jumping)
	{
		jumpDuration += Time.deltaTime;
	}

}

	void Jump()
	{

		if(jumping)
		{
			if(jumpDuration < jumpTime)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
		}
	}

	public void SetPickupValues(GameObject collided)
	{
		canCarry = true;
		carryObject = collided.gameObject;
		carryObjectController = carryObject.GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;
		carryObjectRB = carryObject.GetComponent<Rigidbody2D>();
	}

	public void SetDropValues(GameObject collided)
	{
		canCarry = false;
		carryObject = null;
	}


	/*
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player2")
		{
			canCarry = true;
			carryObject = collider.gameObject;
			carryObjectController = carryObject.GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;
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
