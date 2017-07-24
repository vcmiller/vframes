using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UID))]
public class Checkpoint : MonoBehaviour {
    public Material activeMaterial;

    public Material inactiveMaterial { get; private set; }
    public MeshRenderer mesh { get; private set; }
    public int uid { get; private set; }

    [HideInInspector]
    private bool _active;

    public bool active {
        get {
            return _active;
        }
        set {
            _active = value;
            Material[] m = mesh.materials;
            if (value) {
                m[1] = activeMaterial;
            } else {
                m[1] = inactiveMaterial;
            }
            mesh.materials = m;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponentInParent<Player>()) {
            CheckpointController.inst.activeCheckpoint = this;
        }
    }


    // Use this for initialization
    public void Init () {
        uid = GetComponent<UID>().uid;
        mesh = GetComponent<MeshRenderer>();
        inactiveMaterial = mesh.materials[1];
	}
}
