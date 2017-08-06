using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManage : MonoBehaviour {

    public Vector2 targetGravity = new Vector2(0, -32);
    public float rate = 1.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, targetGravity, rate);
        {
            Vector2 grave = Physics2D.gravity.normalized;
            Quaternion rot = grave == Vector2.zero ? Quaternion.identity : Quaternion.FromToRotation(Vector2.down, grave);
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                player.transform.rotation = rot;
        }
    }
}
