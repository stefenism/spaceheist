using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerController : PlayerControllerBase {

    public float horizontalInfluence = 40f;
    public float maxHorizotalVelocity = 2f;
    public float jumpForce = 12f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        if (Input.GetAxis("HorizontalGreen") != 0 && Mathf.Abs(rb.velocity.x) < maxHorizotalVelocity)
            rb.AddForce(horizontalInfluence * Input.GetAxis("HorizontalGreen") * Vector2.right, ForceMode2D.Force);
        if (grounded && Input.GetAxis("JumpGreen") != 0)
            rb.AddForce(-jumpForce * Physics2D.gravity.normalized, ForceMode2D.Impulse);
        Physics2D.IgnoreLayerCollision(12, 8);
    }

    void FixedUpdate()
    {
      if(canCarry)
  		{
  			if(Input.GetButtonDown("PickupGreen"))
  			{
  				Carry();
  			}
  		}

  		if(carrying)
  		{
  			if(Input.GetButtonDown("PickupGreen") && !beingCarried)
  			{
  				Drop();
  			}

  	}
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
