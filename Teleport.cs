using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    public AudioClip TeleportSFX;
    private AudioSource source;
    public WipeTransition wipe;


    void Start()
    {

      source = GetComponent<AudioSource>();
      if(GameObject.FindWithTag("wipe") != null)
      {
        wipe = GameObject.FindWithTag("wipe").GetComponent<WipeTransition>();
      }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        wipe.activated = true;
        source.PlayOneShot(TeleportSFX);
    }
}
