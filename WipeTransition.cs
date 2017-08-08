using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WipeTransition : MonoBehaviour {


	public bool activated = false;
	public bool scaleDown = false;
	public float widthh = 0;
	public float heighth = 0;
	private float maxScale = 5;

	private Image thisImage;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(transform.parent.parent.gameObject);

		thisImage = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {

		if(activated)
		{
			ScaleUp();
		}

		if(scaleDown)
		{
			ScaleDown();
		}
	}

	void ScaleUp()
	{



		widthh += (Time.deltaTime * 4);
		heighth += (Time.deltaTime * 4);

		thisImage.rectTransform.localScale = new Vector2(widthh, heighth);


		if(widthh >= maxScale)
		{
			activated = false;
			scaleDown = true;

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

	}

	void ScaleDown()
	{
		if(widthh <= 0)
		{
			widthh = 0;
			heighth = 0;

			thisImage.rectTransform.localScale = new Vector2(widthh, heighth);
			scaleDown = false;
			return;
		}

		else
		{
			widthh -= (Time.deltaTime * 4);
			heighth -= (Time.deltaTime * 4);

			thisImage.rectTransform.localScale = new Vector2(widthh, heighth);
		}
	}
}
