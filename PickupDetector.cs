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
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, playerDistance, playerLayer);

		Debug.DrawRay(transform.position, -Vector2.up * playerDistance);

		if(hit.collider != null)
		{

			if(hit.collider.gameObject.tag == "Player")
			{
				player.SetPickupValues(hit.collider.gameObject);
			}

			else
			{
				player.SetDropValues(hit.collider.gameObject);
			}
		}
	}

	}
