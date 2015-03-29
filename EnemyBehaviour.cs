using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public int aiSpeed;
	
	public int jumpValue;
	
	public Rigidbody2D rb;
	
	public Transform verLineStart, verLineEnd;//vertical raycasting
	public Transform horLineStart, horLineEnd;//vertical raycasting
	public Transform horJumpLineStart, horJumpLineEnd;//vertical raycasting
	
	private bool aiGrounded = false;
	private bool verLineEmpty;
	private bool aiTurn;
	private float aiTurnCd;
	private RaycastHit2D checkPlayer;
	
	private bool jumpToPlayer;

	// Use this for initialization
	void Start () {
		rb = transform.rigidbody2D;
		if(Random.value <= 0.33)
		{
			jumpToPlayer = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Raycasting ();
		aiMovement ();
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
		{
			aiGrounded = true;
		}
		if (other.gameObject.tag == "Enemy")
		{
			aiSpeed -= (aiSpeed * 2);
		}
		if (other.gameObject.tag == "-Health")
		{
			Destroy(this.gameObject);
		}
		if(other.gameObject.tag == "Bouncy")
		{
			aiGrounded = false;
			rb.AddForce( new Vector3(0, 350, 0));
		}
	}
	void aiMovement()
	{
		if(verLineEmpty == false && aiGrounded)
		{
			aiGrounded = false;
			rb.AddForce( new Vector3(0, jumpValue, 0));
		}
		if(aiTurn && aiTurnCd <= 0)
		{
			aiSpeed -= (aiSpeed * 2);
			aiTurn = false;
			aiTurnCd = 1;
		}
		else
		{
			aiTurnCd -= Time.deltaTime;
		}
		if(checkPlayer && aiGrounded)
		{
			aiGrounded = false;
			rb.AddForce( new Vector3(0, 350, 0));
		}
		transform.Translate (Vector2.right * aiSpeed * Time.deltaTime);
	}
	void Raycasting()
	{
		Debug.DrawLine( verLineStart.position, verLineEnd.position, Color.green);
		verLineEmpty = Physics2D.Linecast (verLineStart.position, verLineEnd.position, 1 << LayerMask.NameToLayer("RayCheck"));
		
		Debug.DrawLine(horLineStart.position, horLineEnd.position, Color.green);
		aiTurn = Physics2D.Linecast (horLineStart.position, horLineEnd.position, 1 << LayerMask.NameToLayer("RayCheck"));
		if(jumpToPlayer)
		{
			Debug.DrawLine(horLineStart.position, horLineEnd.position, Color.green);
			checkPlayer = Physics2D.Linecast (horJumpLineStart.position, horJumpLineEnd.position, 1 << LayerMask.NameToLayer("PlayerCheck"));
			if (checkPlayer && transform.position.x < checkPlayer.transform.position.x)
			{
				if(aiSpeed < 0)
				{
					aiSpeed -= (aiSpeed * 2);
				}
			}
			if (checkPlayer && transform.position.x > checkPlayer.transform.position.x)
			{
				if(aiSpeed > 0)
				{
					aiSpeed -= (aiSpeed * 2);
				}
			}
		}
	}
}