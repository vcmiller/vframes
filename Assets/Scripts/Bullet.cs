using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public bool primed { get; private set; }
    public float range = 100;

    // Use this for initialization
    void Awake() {
        Ticker.inst.lateTickEvent += LateTick;
    }

    void LateTick() {
        if (primed) {
            transform.position += transform.forward * range;
            GetComponent<CapsuleTrail>().Move();
            Destroy(gameObject, 0.5f);
        } else {
            primed = true;
        }
    }

    private void OnDestroy() {
        Ticker.inst.lateTickEvent -= LateTick;
    }

    private void OnTriggerEnter(Collider other) {
        other.SendMessageUpwards("Hit", SendMessageOptions.DontRequireReceiver);
    }
}
