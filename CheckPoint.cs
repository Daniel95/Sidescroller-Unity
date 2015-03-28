using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public static Transform checkPoint;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
