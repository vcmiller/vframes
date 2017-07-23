using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UID : MonoBehaviour {
    public int uid;
    public bool newUid;

    private void Update() {
        if (Application.isEditor && !Application.isPlaying) {
            if (newUid) {
                newUid = false;
                uid = Random.Range(0, int.MaxValue);
            }
        }
    }
}
