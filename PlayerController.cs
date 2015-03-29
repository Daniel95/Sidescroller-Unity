using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//jumping
	public int jumpSpeed;//Speed
	private float jumpCD;
	private float jumpValue;//stores the value of jump

	private bool grounded = false;
	
	private bool bounce;

	//move Hor
	public int runSpeed;
	private float runValue;//stores the value of run
	private bool runForward = true;//stores what direction you move

	//health
	public int playerHealth;
	private int healthBonus;//stores how many shrooms you picked up
	private float damagedCd;

	//sound
	public AudioClip checkPointSound;
	public AudioClip getDamagedSound;
	public AudioClip killEnemySound;
	public AudioClip hearthSound;
	

	//restarting
	public int maxFallDistance;
	private bool retryButton = false;
	private bool finished = false;

	//other
	public Transform checkPoint;//stores what checkpoint you last touched

	public Texture2D hearth;//health indicator

	private Rigidbody2D rb;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = transform.rigidbody2D;
		this.transform.position = checkPoint.transform.position;
		//_parallaxController = GetComponent<ParallaxController> ();
		//this.transform.rotation.z = 0;
		//rigidbody2D.fixedAngle = true;
	}

	void Update() 
	{
		Controls ();
		fallToDeath ();
	}
	//All Triggers
	void OnTriggerEnter2D(Collider2D other)//triggers
	{
		if (other.gameObject.tag == "Checkpoint") {
			checkPoint = other.gameObject.transform;
			audio.clip = checkPointSound;
			audio.Play();
		}
		if (other.gameObject.tag == "+Health")
		{
			playerHealth += 1;
			healthBonus += 1;	
			Destroy(other.gameObject);
			audio.clip = hearthSound;
			audio.Play();
		}
	}

	//All Collisions
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
		{
			anim.SetInteger("AnimationState",0);
			grounded = true;
			bounce = false;
			runValue = runSpeed;
			jumpValue = jumpSpeed;
		}
		if(other.gameObject.tag == "Bouncy")
		{
			anim.SetInteger("AnimationState",1);
			grounded = false;
			bounce = true;
			rb.AddForce( new Vector3(0, 400, 0));
		}
		if(other.gameObject.tag == "BigBouncy")
		{
			anim.SetInteger("AnimationState",1);
			grounded = false;
			bounce = true;
			rb.AddForce( new Vector3(0, 800, 0));
		}
		if(other.gameObject.tag == "End")
		{
			print("KK");
			rb.AddForce( new Vector3(0, 1500, 0));
			finished = true;
		}
		if (transform.position.y - (transform.localScale.y / 2) > other.transform.position.y && other.gameObject.tag == "Enemy")//jumpkill enemy
		{
			Destroy(other.gameObject);
			rb.AddForce( new Vector3(0, 250, 0));
			audio.clip = killEnemySound;
			audio.Play();
		}
		else{
			if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "-Health" && damagedCd <= 0)//1 leven eraf
			{	
				audio.clip = getDamagedSound;
				audio.Play();
				anim.SetInteger("AnimationState",3);
				grounded = false;
				playerHealth--;
				damagedCd = 0.1f;
				rb.AddForce( new Vector3(0, 240, 0));
				if(playerHealth <= 0)
				{
					//rigidbody2D.fixedAngle = false;
					retryButton = true;
					//transform.rotation = Quaternion.identity;
					//Destroy(this.collider);
					//Physics.IgnoreCollision(this.GetComponent<Collider>(), GetComponent<Collider>());
				}
			}
		}
		if(damagedCd > 0)
		{
			damagedCd -= Time.deltaTime;
		}
	}

	//Input Controls &  Input Movement Modifications
	void Controls()
	{
		if (playerHealth > 0)
		{
			if(jumpCD > 0)
			{
				jumpCD -= Time.deltaTime;//Jump CD
			}
			if (Input.GetKey (KeyCode.Space) && grounded && jumpCD <= 0) {///jumping (SPACE)
				rb.AddForce (new Vector2 (0, jumpValue));
				grounded = false;
				anim.SetInteger ("AnimationState", 2);
				jumpCD = 0.30f;
			}
			if (grounded == false) {
				if (!bounce) {
					runValue -= runValue / 3 * Time.deltaTime;//moves slower when in the air
				}
				if (Input.GetKey (KeyCode.S)) {
					rb.AddForce (new Vector2 (0, -1500 * Time.deltaTime));//dash down
				}
			}
			if (Input.GetKey (KeyCode.A)) {
				if (runForward) {
					transform.localScale = new Vector3 (-1, 1, 1);
				}
				anim.SetInteger ("AnimationState", 1);
				runForward = false;
				rb.AddForce (new Vector2 (-runValue * Time.deltaTime, 0));
			}
			if (Input.GetKey (KeyCode.D)) {
				if (!runForward) {
					transform.localScale = new Vector3 (1, 1, 1);
				}
				anim.SetInteger ("AnimationState", 1);
				runForward = true;
				rb.AddForce (new Vector2 (runValue * Time.deltaTime, 0));
			}
			if(!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && grounded)
			{
				anim.SetInteger ("AnimationState", 0);
			}
		}
	}

	//Player falls to death
	void fallToDeath()
	{
		if (transform.position.y <= maxFallDistance)//dead by falling
		{
			playerHealth--;
			retryButton = true;
		}
	}

	//Menu
	void OnGUI()
	{
		for(int i = 1;i <= playerHealth; i++)
		{
			GUI.DrawTexture(new Rect(30*i,10,30,30), hearth);
		}
		if(retryButton)
		{
			GUI.Box (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 25), "You Lose!!");
			if (GUI.Button (new Rect ((Screen.width / 2 - 75), Screen.height / 2 - 20, 150, 40), "Click R to Restart") || Input.GetKey (KeyCode.R))
			{
				this.transform.position = checkPoint.transform.position;
				retryButton = false;
				playerHealth = 2 + healthBonus;
			}
		}
		if(finished)
		{
			GUI.Box (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 50, 160, 25), "You Win!!");
			if (GUI.Button (new Rect ((Screen.width / 2 - 40), Screen.height / 2 - 20, 100, 40), "Again? (R)") || Input.GetKey (KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}
