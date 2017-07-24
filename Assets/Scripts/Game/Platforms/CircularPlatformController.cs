
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatformController : MovingPlatformController {
    public int centerX = 1;
    public int centerZ = 0;
    public Vector3 center { get; private set; }
    public int dir = 1;

    // Use this for initialization
    void Awake() {
        center = transform.position + (transform.right * centerX + transform.forward * centerZ) * 10.0f;
	}

    public override Vector3 GetNextPos() {
        Vector3 v = transform.position - center;
        v = Quaternion.Euler(0, 90 * dir, 0) * v;
        return center + v;
    }
}
