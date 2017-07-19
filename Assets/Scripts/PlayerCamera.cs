using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Vector3 position { get; private set; }
    public Vector3 offsetFromPlayer { get; private set; }
    public Quaternion rotation { get; private set; }

    public float moveSpeed = 20;

	// Use this for initialization
	void Start () {
        position = transform.position;
        offsetFromPlayer = transform.position - transform.parent.position;
        rotation = transform.rotation;
	}

    private void LateUpdate() {
        transform.rotation = rotation;

        position = Vector3.MoveTowards(position, transform.parent.position + offsetFromPlayer, moveSpeed * Time.deltaTime);
        transform.position = position;
    }
}
