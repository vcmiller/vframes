using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearPlatformController : MovingPlatformController {
    public int distance = 4;
    public int direction;

    private int dir = 1;
    private int rem;
    private Vector3 moveDir;

    // Use this for initialization
    void Start() {
        float angle = direction * 45.0f;
        moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

        moveDir.x = Mathf.Round(moveDir.x);
        moveDir.y = Mathf.Round(moveDir.y);

        rem = distance;
    }

    public override Vector3 GetNextPos() {
        rem--;
        Vector3 vec = transform.position + moveDir * dir * 10.0f;

        if (rem == 0) {
            dir *= -1;
            rem = distance;
        }

        return vec;
    }
}
