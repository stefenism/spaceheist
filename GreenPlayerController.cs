using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerController : PlayerControllerBase {

    public float horizontalInfluence = 40f;
    public float maxHorizotalVelocity = 2f;
    public float jumpForce = 12f;
    public float brakeInfluence = 2f;
    public AudioClip GreenJump;
    public AudioClip GreenThrow;

    //private AudioSource source;
    private float Vollow = 0.5f;
    private float VolHigh = 1.0f;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        anim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
    }


    void Update () {
        float joymot = Input.GetAxis("HorizontalGreen");
        if (Input.GetAxis("HorizontalGreen") == 0)
            rb.AddForce(-brakeInfluence * Vector2.Dot(rb.velocity, transform.right) * transform.right, ForceMode2D.Force);
        else if (Mathf.Abs(Vector2.Dot(rb.velocity, transform.right)) < maxHorizotalVelocity)
            rb.AddForce(horizontalInfluence * joymot * transform.right, ForceMode2D.Force);
        if (grounded && Input.GetAxis("JumpGreen") != 0){
            rb.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
            source.PlayOneShot(GreenJump);
          }

        Physics2D.IgnoreLayerCollision(12, 8);


        if(joymot > 0 && !facingRight)
  			{
  				Flip();
  			}
  			else if(joymot < 0 && facingRight)
  			{
  				Flip();
  			}

        if(beingCarried)
    		{
    			if(Input.GetButtonDown("JumpGreen"))
    			{
    				if(transform.parent != null)
    				{
    					transform.parent = null;
    				}
    				beingCarried = false;
    				GetComponent<BoxCollider2D>().enabled = true;
    				rb.isKinematic = false;

    			}
    		}

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    		anim.SetFloat("vspeed", Mathf.Abs(rb.velocity.y));
    		anim.SetBool("grounded",grounded);
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("JumpGreen"))
        {
            source.PlayOneShot(GreenJump);
        }
        if (canCarry)
  		  {
  			if(Input.GetButtonDown("PickupGreen"))
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

  			if(Input.GetButtonDown("ThrowGreen") && !beingCarried)
  			{
          source.PlayOneShot(GreenThrow);
  				Throw();
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

  	public void SetDropValues()
  	{
  		canCarry = false;
  		carryObject = null;
  	}

    /*
    public override void SetPickupValues(GameObject collided)
  	{
  		canCarry = true;
  		carryObject = collided.gameObject;
  		carryObjectController = carryObject.GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;
  		carryObjectRB = carryObjectRB.GetComponent<Rigidbody2D>();
  	}

  	public override void SetDropValues(GameObject collided)
  	{
  		canCarry = false;
  		carryObject = null;
  	}

    /*
    void OnTriggerEnter2D(Collider2D collider)
  	{
  		if(collider.gameObject.tag == "Player")
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
