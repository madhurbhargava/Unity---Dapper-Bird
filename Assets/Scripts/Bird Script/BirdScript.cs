using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour {

	public static BirdScript instance;

	[SerializeField]
	private Rigidbody2D birdRigidBody;

	[SerializeField]
	private Animator anim;

	private float forwardSpeed = 3f;

	private float bounceSpeed = 4f;

	private bool didFlap;

	public bool isAlive;

	public int score;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip flapClip, diedClip, pointClip;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}

		isAlive = true;
		score = 0; 

		SetCameraX ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isAlive) {
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			if(didFlap)
			{
				didFlap = false;
				birdRigidBody.velocity = new Vector2(0, bounceSpeed);
				audioSource.PlayOneShot(flapClip);
				anim.SetTrigger("Flap");
			}

			if(birdRigidBody.velocity.y >= 0)
			{
				transform.rotation = Quaternion.Euler(0,0,0);
			}
			else
			{
				float angle = 0;
				angle = Mathf.Lerp(0, -90, -birdRigidBody.velocity.y / 7);
				transform.rotation = Quaternion.Euler(0,0,angle);
			}
		}
	
	}

	void SetCameraX()
	{
		CameraScript.OffsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
	}

	public float GetPositionX()
	{
		return transform.position.x;
	}

	public void FlapTheBird()
	{
		didFlap = true;
	}

	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe") {
			if(isAlive)
			{
				isAlive = false;
				anim.SetTrigger("Bird Died");
				audioSource.PlayOneShot(diedClip);
				//AdsController.instance.ShowInterstitial();
				GameplayController.instance.PlayerDiedShowScore(score);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "PipeHolder") {
			audioSource.PlayOneShot(pointClip);
			score++;
			Debug.Log("XXXXXXOn trigger enter 2d, setting up score");
			GameplayController.instance.SetScore(score);
		}
	}
}
