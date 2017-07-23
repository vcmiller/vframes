using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour {
    public bool useControlRotation = true;

    public ControlProxy control { get; private set; }

	// Use this for initialization
	void Start () {
        control = GetComponentInParent<ControlProxy>();
	}

    private void LateUpdate() {
        if (useControlRotation && control) {
            transform.rotation = control.rotation;
        }
    }
}
