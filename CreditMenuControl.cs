using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenuControl : MonoBehaviour {

	public GameObject creditMenu;
	public GameObject startMenu;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowCredits()
	{
		creditMenu.SetActive(true);
		startMenu.SetActive(false);
	}

	public void ShowStartMenu()
	{
		creditMenu.SetActive(false);
		startMenu.SetActive(true);
	}
}
