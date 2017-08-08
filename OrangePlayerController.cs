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

	private Animator anim;

	//private AudioSource source;
	public AudioClip OrangeJump;
	public AudioClip OrangeThrow;
	private float Vollow = 0.5f;
	private float VolHigh = 1.0f;


	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
		anim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		if(!youDed || !beenThrown)
		{
			CheckInput();
		}

		/*
		if(beenThrown)
		{
			BeingThrown();
		}
		*/


		Physics2D.IgnoreLayerCollision(9,10);

		if(moveDir > 0 && !facingRight)
		{
			Flip();
		}
		else if(moveDir <0 && facingRight)
		{
			Flip();
		}
	}

	void FixedUpdate()
	{
		if(canRun && !cantMove)
		{
            rb.velocity = speed * moveDir * transform.right + Vector2.Dot(rb.velocity, transform.up) * transform.up;
    }

		else if(!cantMove)
		{
			  rb.AddForce(speed * moveDir * transform.right + Vector2.Dot(rb.velocity, transform.up) * transform.up);//) = (speed * .75f) * moveDir * transform.right + Vector2.Dot(rb.velocity, transform.up) * transform.up;
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

			if(Input.GetButtonDown("Throw") && !beingCarried)
			{
				Throw();
				source.PlayOneShot(OrangeThrow);
			}

		}
	}

	void CheckInput()
	{
		if(!beingCarried)
		{
			moveDir = Input.GetAxis("Horizontal");
			//Debug.Log("moveDir: " + moveDir);
		}

		if(beingCarried)
		{
			if(Input.GetButtonDown("Jump"))
			{
				if(transform.parent != null)
				{
					transform.parent = null;
				}
				beingCarried = false;
				GetComponent<BoxCollider2D>().enabled = true;
				rb.isKinematic = false;

				jumpAllowed = true;
				canJump = true;
			}
		}



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
			canRun = false;
		}

		JumpButton();

		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
		anim.SetFloat("vspeed", Mathf.Abs(rb.velocity.y));
		anim.SetBool("grounded",grounded);

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

		if (Input.GetButtonDown("Jump"))
		{
				source.PlayOneShot(OrangeJump);
		}

		if(jumping)
		{
			if(jumpDuration < jumpTime)
			{
                rb.velocity = Vector2.Dot(rb.velocity, transform.right) * transform.right + jumpForce * transform.up;
			}
		}
	}

	public void SetPickupValues(GameObject collided)
	{
		canCarry = true;
		carryObject = collided.gameObject;
		carryObject.transform.localScale = transform.localScale;
		carryObjectController = carryObject.GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;
		carryObjectRB = carryObject.GetComponent<Rigidbody2D>();
	}

	public void SetDropValues()
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
