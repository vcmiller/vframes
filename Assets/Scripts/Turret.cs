using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public Bullet prefab;

	// Use this for initialization
	void Start () {
        Ticker.inst.earlyTickEvent += Fire;
	}
	
	void Fire() {
        transform.forward = FindObjectOfType<DashPlayerController>().transform.position - transform.position;
        Instantiate(prefab.gameObject, transform.position + transform.forward, transform.rotation);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.GetComponent<Player>()) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        Ticker.inst.earlyTickEvent -= Fire;
    }
}
