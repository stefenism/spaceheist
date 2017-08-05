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
    RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, groundDistance, groundLayer);

    Debug.DrawRay(transform.position, -Vector2.up * groundDistance);

    if(hit.collider != null)
    {

      if(hit.collider.gameObject.tag == "Ground" && (player.rb.velocity.y > 5))
      {
        player.grounded = false;
      }

      else
      {
        player.grounded = true;
      }
    }

    else
    {
      player.grounded = false;
    }
  }

  }
