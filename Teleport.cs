using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    public AudioClip TeleportSFX;
    private AudioSource source;




    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        source.PlayOneShot(TeleportSFX);
    }
}
