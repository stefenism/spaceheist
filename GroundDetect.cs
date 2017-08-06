using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour {
  private PlayerControllerBase player;
  public float groundDistance;
  public LayerMask groundLayer;


  // Use this for initialization
  void Start () {

    player = GetComponent(typeof(PlayerControllerBase)) as PlayerControllerBase;

  }

  // Update is called once per frame
  void Update () {

    GroundDetection();
  }

  void GroundDetection()
  {
    Vector3 hit2transform = new Vector3(transform.position.x + .15f, transform.position.y, 0);
    Vector3 hit3transform = new Vector3(transform.position.x - .15f, transform.position.y, 0);

    RaycastHit2D hit = Physics2D.Raycast(transform.position, Physics2D.gravity.normalized, groundDistance, groundLayer);
    RaycastHit2D hit2 = Physics2D.Raycast(hit2transform, Physics2D.gravity.normalized, groundDistance, groundLayer);
    RaycastHit2D hit3 = Physics2D.Raycast(hit3transform, Physics2D.gravity.normalized, groundDistance, groundLayer);

    Debug.DrawRay(transform.position, Physics2D.gravity.normalized * groundDistance);
    Debug.DrawRay(hit2transform, Physics2D.gravity.normalized * groundDistance);
    Debug.DrawRay(hit3transform, Physics2D.gravity.normalized * groundDistance);

    if(hit.collider != null || hit2.collider != null || hit3.collider != null)
    {
      player.grounded = true;

      if (hit.collider != null)
      {
        if(groundLayer == (groundLayer | (1 << hit.collider.gameObject.layer)) && (Vector2.Dot(player.rb.velocity, -Physics2D.gravity.normalized) > 5f))
        {
          player.grounded = false;
        }
      }

      if(hit2.collider != null)
      {
        if(groundLayer == (groundLayer | (1 << hit2.collider.gameObject.layer)) && (Vector2.Dot(player.rb.velocity, -Physics2D.gravity.normalized) > 5f))
        {
          player.grounded = false;
        }
      }

      if(hit3.collider != null)
      {
        if(groundLayer == (groundLayer | (1 << hit3.collider.gameObject.layer)) && (Vector2.Dot(player.rb.velocity, -Physics2D.gravity.normalized) > 5f))
        {
          player.grounded = false;
        }
      } 
    }

    else
    {
      player.grounded = false;
    }
  }

  }
