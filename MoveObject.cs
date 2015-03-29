using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {

	public bool hor;

	public float speed;

	public float liveTime;

	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		liveTime -= Time.deltaTime;
		if(liveTime <= 0)
		{
			Destroy(this.gameObject);
		}
		liveTime -= 1 * Time.deltaTime;
		if(hor)
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		else
		{
			transform.Translate (Vector2.up * speed * Time.deltaTime);
		}
	}
}
