using UnityEngine;
using System.Collections;
	
public class RotateObject : MonoBehaviour {

	
	public int rotateSpeed;

	void Update() {
		transform.Rotate(0,0,rotateSpeed*Time.deltaTime);
	}
}