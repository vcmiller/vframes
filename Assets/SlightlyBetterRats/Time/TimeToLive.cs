﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour {
    public float timeToLive;

    private void Update() {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0) {
            Destroy(gameObject);
        }
    }
}
