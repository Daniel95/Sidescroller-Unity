using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public int aiSpeed;

	public int jumpValue;

	public Rigidbody2D rb;

	public Transform verLineStart, verLineEnd;//vertical raycasting
	public Transform horLineStart, horLineEnd;//vertical raycasting

	public bool aiGrounded = false;
	public bool verLineEmpty;
	public bool aiTurn;



	// Use this for initialization
	void Start () {
		rb = transform.rigidbody2D;
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
		if(other.gameObject.tag == "Bouncy")
		{
			aiGrounded = false;
			rb.AddForce( new Vector3(0, 200, 0));
		}
	}
	void aiMovement()
	{
		if(verLineEmpty == false && aiGrounded)
		{
			aiGrounded = false;
			rb.AddForce( new Vector3(0, jumpValue, 0));
		}
		if(aiTurn == true)
		{
			aiSpeed -= (aiSpeed * 2);
		}
		transform.Translate (Vector2.right * aiSpeed * Time.deltaTime);
	}
	void Raycasting()
	{
		Debug.DrawLine( verLineStart.position, verLineEnd.position, Color.green);
		verLineEmpty = Physics2D.Linecast (verLineStart.position, verLineEnd.position, 1 << LayerMask.NameToLayer("RayCheck"));

		Debug.DrawLine(horLineStart.position, horLineEnd.position, Color.green);
		aiTurn = Physics2D.Linecast (horLineStart.position, horLineEnd.position, 1 << LayerMask.NameToLayer("RayCheck"));
	}
}
