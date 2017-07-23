using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPlatformController : MovingPlatformController {
    public Vector3[] waypoints { get; private set; }
    public Transform path;
    public int startIndex;

    private int curDest;

    private void Start() {

        waypoints = new Vector3[path.childCount + 1];
        for (int i = 0; i < path.childCount; i++) {
            waypoints[i] = path.GetChild(i).position;
        }

        waypoints[path.childCount] = transform.position;

        curDest = startIndex;
    }

    public override Vector3 GetNextPos() {
        Vector3 curDestPoint = waypoints[curDest];
        Vector3 toDest = (curDestPoint - transform.position).normalized;


        toDest.x = Mathf.Round(toDest.x);
        toDest.z = Mathf.Round(toDest.z);

        toDest *= 10;

        Vector3 target = transform.position + toDest;

        if (Vector3.SqrMagnitude(target - curDestPoint) < 25) {
            curDest++;
            curDest %= waypoints.Length;
        }

        return target;
    }
}
