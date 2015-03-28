using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {

	private bool rbAttached = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -20)//doodgaan
		{
			Destroy(this);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(rbAttached == false){
			Rigidbody2D rBody = this.gameObject.AddComponent<Rigidbody2D> (); // Add the rigidbody.
			rbAttached = true;
		}
	}
}
