using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public Bullet prefab;
    public GameObject fired;

	// Use this for initialization
	void Start () {
        Ticker.earlyTickEvent += Fire;
	}
	
	void Fire() {
        DashPlayerController target = FindObjectOfType<DashPlayerController>();
        if (target) {
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0;
            transform.forward = dir;
        }

        fired = Instantiate(prefab.gameObject, transform.position + transform.forward + Vector3.up, transform.rotation);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.GetComponent<Player>()) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        Destroy(fired);
        Ticker.earlyTickEvent -= Fire;
    }
}
