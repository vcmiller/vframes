using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleTrail : MonoBehaviour {
    public Transform capsule { get; private set; }
    public Vector3 lastPos { get; private set; }

    public Vector3 offset;
    public float leaveOutDistance = 1;

    // Use this for initialization
    void Awake () {
        capsule = GetComponentInChildren<CapsuleCollider>().transform;

        lastPos = transform.position;

        enabled = false;
    }

    public void Move() {
        enabled = true;

        Vector3 position = transform.position + offset;
        Vector3 oldPosition = Vector3.MoveTowards(lastPos, position, leaveOutDistance);

        capsule.position = (position + oldPosition) / 2;
        capsule.up = oldPosition - position;
        capsule.localScale = new Vector3(0.1f, Vector3.Distance(position, oldPosition) / 2, 0.1f);

        lastPos = position;

        Invoke("Disable", 0.1f);
    }

    void Disable() {
        enabled = false;
    }

    private void OnEnable() {
        capsule.gameObject.SetActive(true);
    }

    private void OnDisable() {
        capsule.gameObject.SetActive(false);
    }
}
