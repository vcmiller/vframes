using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour {
    public Vector3 offsetFromCamera { get; private set; }
    public float followRatio;
    public BoxCollider boundsObject;

    public Transform camera;

	// Use this for initialization
	void Start () {
        offsetFromCamera = transform.position - camera.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = camera.position * followRatio + offsetFromCamera;

        Bounds bounds = boundsObject.bounds;
        float w = bounds.size.x / 2;
        float h = bounds.size.z / 2;

        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);

            Vector3 off = child.position - bounds.center;

            while (off.x < -w) {
                off.x += w * 2;
            }

            while (off.z < -h) {
                off.z += h * 2;
            }

            while (off.x > w) {
                off.x -= w * 2;
            }

            while (off.z > h) {
                off.z -= h * 2;
            }

            child.transform.position = bounds.center + off;

            Vector3 v = child.transform.localPosition;
            v.x = Mathf.Round(v.x);
            v.z = Mathf.Round(v.z);
            child.transform.localPosition = v;
        }
	}
}
