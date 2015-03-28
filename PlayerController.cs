using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	/*
	public float horSpeed;
	public float verSpeed;
	public float horplusMoveSpeed;
	public float verplusMoveSpeed;
	*/

	private Vector3 relativePosition;

	private float isFallingTimer;
	private float fallRotation;

	private bool runForward = true;

	public float jumpCD;
	private float jumpValue;
	public int jumpSpeed;
	public int runSpeed;
	private float runValue;
	public int playerHealth;
	private int healthBonus;

	public Texture2D hearth;

	private bool bounce;

	public Transform checkPoint;

	public int maxFallDistance;
	private bool retryButton = false;
	private bool finished = false;

	private float run;

	private bool grounded = false;

	public Rigidbody2D rb;

	Animator anim;

	//private ParallaxController _parallaxController;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = transform.rigidbody2D;
		this.transform.position = checkPoint.transform.position;
		//_parallaxController = GetComponent<ParallaxController> ();
		//this.transform.rotation.z = 0;
		//rigidbody2D.fixedAngle = true;
	}
	/*
	void FixedUpdate() {
		Controls ();
		fallToDeath ();

		print (grounded);
	}
	*/
	void Update() 
	{
		Controls ();
		fallToDeath ();
		
		print (grounded);
	}
	//All Triggers
	void OnTriggerEnter2D(Collider2D other)//triggers
	{
		if (other.gameObject.tag == "Checkpoint") {
			checkPoint = other.gameObject.transform;
		}
		if (other.gameObject.tag == "+Health")
		{
			playerHealth += 1;
			healthBonus += 1;	
			Destroy(other.gameObject);
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
		if(other.gameObject.tag == "End")
		{
			rb.AddForce( new Vector3(0, 1500, 0));
			finished = true;
		}
		if (transform.position.y - (transform.localScale.y / 2) > other.transform.position.y && other.gameObject.tag == "Enemy")//jumpkill enemy
		{
			Destroy(other.gameObject);
			rb.AddForce( new Vector3(0, 250, 0));
		}
		else{
			if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "-Health")//1 leven eraf
			{	
				anim.SetInteger("AnimationState",3);
				playerHealth--;
				rb.AddForce( new Vector3(0, 400, 0));
				if(playerHealth <= 0)
				{
					//rigidbody2D.fixedAngle = false;
					rb.AddForce( new Vector3(0, 400, 0));
					retryButton = true;
					//transform.rotation = Quaternion.identity;
					//Destroy(this.collider);
					//Physics.IgnoreCollision(this.GetComponent<Collider>(), GetComponent<Collider>());
				}
			}
		}
	}

	//Input Controls &  Input Movement Modifications
	void Controls()
	{
		if (playerHealth > 0)
		{
			jumpCD -= Time.deltaTime;//Jump CD
			if (Input.GetKey (KeyCode.Space) && grounded && jumpCD <= 0) {///Springen
				rb.AddForce (new Vector2 (0, jumpValue));
				grounded = false;
				anim.SetInteger ("AnimationState", 2);
				jumpCD = 0.4f;
			}
			if (grounded == false) {
				if (!bounce) {
					runValue -= runValue / 3 * Time.deltaTime;//langzamer opzij als je in de lucht bent
				}
				if (Input.GetKey (KeyCode.S)) {
					anim.SetInteger ("AnimationState", 0);
					rb.AddForce (new Vector2 (0, -1000 * Time.deltaTime));//naar beneden als je sprint
				}
			}
			if (Input.GetKey (KeyCode.A)) {
				if (grounded) {
					anim.SetInteger ("AnimationState", 1);
				}
				if (runForward) {
					transform.localScale = new Vector3 (-1, 1, 1);
				}
				runForward = false;
				rb.AddForce (new Vector2 (-runValue * Time.deltaTime, 0));
			}
			if (Input.GetKey (KeyCode.D)) {
				if (grounded) {
					anim.SetInteger ("AnimationState", 1);
				}
				if (!runForward) {
					transform.localScale = new Vector3 (1, 1, 1);
				}
				runForward = true;
				rb.AddForce (new Vector2 (runValue * Time.deltaTime, 0));
			}
			if (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) {
				anim.SetInteger ("AnimationState", 0);
			}
		}
	}

	//Player falls to death
	void fallToDeath()
	{
		if (transform.position.y <= maxFallDistance)//doodgaan
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
			GUI.Box (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 25), "You Win!!");
			if (GUI.Button (new Rect ((Screen.width / 2 - 50), Screen.height / 2 - 20, 100, 40), "Again?"))
			{
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}
