using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playhandler : MonoBehaviour {

    public GameObject panel;
    public WipeTransition wipe;

    void Awake()
    {
      panel = transform.parent.gameObject;

      if(GameObject.FindWithTag("wipe") != null)
      {
        wipe = GameObject.FindWithTag("wipe").GetComponent<WipeTransition>();
      }
    }
    public void playGame()
    {
        wipe.activated = true;
        panel.SetActive(false);

    }

    public void SkipLevel()
    {
      wipe.activated = true;
      panel.SetActive(false);
    }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
