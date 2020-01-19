using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed;
	float currentTime = 0f;
	float startingTime = 12f;
	
	public Text countText;
	public Text winText;
	public Text gameOverText;
	
	public AudioSource musicSource;
	public AudioClip musicClipOne;
	public AudioClip musicClipTwo;
	public AudioClip musicClipThree;

	private bool win;
	private bool gameOver;
	private int count;
	private Rigidbody2D rb2d;
	[SerializeField] Text countdownText;

	void Start ()
	{
		currentTime = startingTime;
		gameOver = false;
		win = false;
		gameOverText.text = "";
		winText.text = "";
		rb2d = GetComponent<Rigidbody2D> ();
		count = 0;
		SetCountText ();
	}
	
	void Update()
    {
        currentTime -= 1 * Time.deltaTime;
		countdownText.text = currentTime.ToString("Timer: 0");
		
		if(currentTime <= 0) 
		{
			currentTime = 0;
		}
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal,moveVertical);
		rb2d.AddForce (movement * speed);
		{
			if (Input.GetKey("escape"))
			{
		
				Application.Quit();
			}
		}
	}

	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			musicSource.clip = musicClipTwo;
			musicSource.Play();
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count == 10 && currentTime > 0)
		{
			winText.text = "You Win!";
			gameOver = true;
			musicSource.clip = musicClipOne;
			musicSource.Play();
		}
		
		if (count <= 10 && currentTime == 0)
		{
			gameOverText.text = "Game Over!";
			gameOver = true;
			musicSource.clip = musicClipThree;
			musicSource.Play();
		}
	}
}