using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour {

	// Use this for initialization
	void Awake () {
			DontDestroyOnLoad(transform.gameObject);
	}

	// Update is called once per frame
	void Update () {

	}
}
