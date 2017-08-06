using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTweak : MonoBehaviour {

    public GameObject boss;
    public Vector2 targetGravity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            boss.GetComponent<GravityManage>().targetGravity = targetGravity;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
