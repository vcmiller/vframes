using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleTrail : MonoBehaviour {
    public Transform capsule { get; private set; }
    public Vector3 lastPos { get; private set; }

    // Use this for initialization
    void Awake () {
        capsule = GetComponentInChildren<CapsuleCollider>().transform;

        lastPos = transform.position;

        enabled = false;
    }

    public void Move() {
        enabled = true;

        capsule.position = (transform.position + lastPos) / 2;
        capsule.up = lastPos - transform.position;
        capsule.localScale = new Vector3(0.1f, Vector3.Distance(transform.position, lastPos) / 2, 0.1f);

        lastPos = transform.position;

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
