using UnityEngine;
using System.Collections;

public class MovebackforthObject : MonoBehaviour {

	public float moveSpeed;
	public float backAndForth;
	private float startPoint;
	//private float startPointZ;

	public bool hor;

	// Use this for initialization
	void Start () {
		if(hor)
		{
			startPoint = transform.position.x;
		}
		else
		{
			startPoint = transform.position.y;
		}
		//startPointZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if(hor)
		{
			if(transform.position.x > (startPoint + backAndForth) /*|| transform.position.y > (startPointZ + backAndForth)*/)
			{
				moveSpeed -= (moveSpeed * 2);
			}
			if(transform.position.x < (startPoint - backAndForth) /*|| transform.position.y < (startPointZ - backAndForth)*/)
			{
				moveSpeed -= (moveSpeed * 2);
			}
			transform.Translate(moveSpeed, 0, 0);
		}
		else
		{
			if(transform.position.y > (startPoint + backAndForth) /*|| transform.position.y > (startPointZ + backAndForth)*/)
			{
				moveSpeed -= (moveSpeed * 2);
			}
			if(transform.position.y < (startPoint - backAndForth) /*|| transform.position.y < (startPointZ - backAndForth)*/)
			{
				moveSpeed -= (moveSpeed * 2);
			}
			transform.Translate(0, moveSpeed, 0);
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "-Health")
		{
			Destroy(this.gameObject);
		}
	}


}
