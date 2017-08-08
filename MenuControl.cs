using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

	public GameObject menu;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(menu.activeSelf)// = true)
			{
				menu.SetActive(false);
			}
			else
			{
				menu.SetActive(true);
			}
		}
	}
}
