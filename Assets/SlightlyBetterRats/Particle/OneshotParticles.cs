﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneshotParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
	}
}
