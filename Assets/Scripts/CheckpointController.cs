using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
    public static CheckpointController inst { get; private set; }
    private static int _activeCheckpoint = -1;

    private Dictionary<int, Checkpoint> checkpoints;

    public Checkpoint activeCheckpoint {
        get {
            if (checkpoints.ContainsKey(_activeCheckpoint)) {
                return checkpoints[_activeCheckpoint];
            } else {
                return null;
            }
        } set {
            if (checkpoints.ContainsKey(_activeCheckpoint)) {
                Checkpoint active = checkpoints[_activeCheckpoint];
                if (active) {
                    active.active = false;
                }
            }
            

            if (value && checkpoints.ContainsKey(value.uid)) {
                _activeCheckpoint = value.uid;
                value.active = true;
            } else {
                _activeCheckpoint = -1;
            }
        }
    }

    // Use this for initialization
    void Awake () {
        inst = this;
        checkpoints = new Dictionary<int, Checkpoint>();
        print(_activeCheckpoint);

        foreach (Checkpoint c in FindObjectsOfType<Checkpoint>()) {
            c.Init();
            checkpoints[c.uid] = c;

            if (_activeCheckpoint == c.uid) {
                c.active = true;

                FindObjectOfType<Player>().transform.position = c.transform.position;
            }
        }

        if (!activeCheckpoint) {
            _activeCheckpoint = -1;
        }
	}


}
