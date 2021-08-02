using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour {
	public float time;
	// Use this for initialization
	void Start () {
		Invoke ("SelfDestruct", time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelfDestruct(){
		Destroy (gameObject);
	}

}
