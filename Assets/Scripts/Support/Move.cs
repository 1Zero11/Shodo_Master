﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public Vector3 direction;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * Time.deltaTime, Space.World);
	}
}
