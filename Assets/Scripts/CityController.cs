using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour {
    public Vector3 offsetFromCamera { get; private set; }
    public float followRatio;

    public Transform camera;

	// Use this for initialization
	void Start () {
        offsetFromCamera = transform.position - camera.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = camera.position * followRatio + offsetFromCamera;

        print(camera.position * followRatio);
	}
}
