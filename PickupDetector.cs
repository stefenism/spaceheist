using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDetector : MonoBehaviour {
	private PlayerControllerBase player;
	public float playerDistance;
	public LayerMask playerLayer;


	// Use this for initialization
	void Start () {

		player = GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;

	}

	// Update is called once per frame
	void Update () {

		PlayerDetection();
	}

	void PlayerDetection()
	{
		Vector3 hit2transform = new Vector3(transform.position.x + .15f, transform.position.y, 0);
    Vector3 hit3transform = new Vector3(transform.position.x - .15f, transform.position.y, 0);

		RaycastHit2D hit2 = Physics2D.Raycast(hit2transform, Physics2D.gravity.normalized, playerDistance, playerLayer);
    RaycastHit2D hit3 = Physics2D.Raycast(hit3transform, Physics2D.gravity.normalized, playerDistance, playerLayer);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, playerDistance, playerLayer);

		Debug.DrawRay(transform.position, -Vector2.up * playerDistance);
		Debug.DrawRay(hit2transform, -transform.up * playerDistance);
		Debug.DrawRay(hit3transform, -transform.up * playerDistance);

		if(hit.collider != null || hit2.collider != null || hit3.collider != null)
		{
			player.SetDropValues();

			if(hit.collider != null)
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					player.SetPickupValues(hit.collider.gameObject);
				}

			}

			if(hit2.collider != null)
			{
				if(hit2.collider.gameObject.tag == "Player")
				{
					player.SetPickupValues(hit2.collider.gameObject);
				}
			}

			if(hit3.collider != null)
			{
				if(hit3.collider.gameObject.tag == "Player")
				{
					player.SetPickupValues(hit3.collider.gameObject);
				}
			}
		}
	}
}
