using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingPlatformController : MonoBehaviour {
    public abstract Vector3 GetNextPos();
}
