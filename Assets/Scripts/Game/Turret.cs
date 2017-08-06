using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public Bullet prefab;
    public Transform barrel;
    public Animator animator { get; private set; }
    public GameObject fired { get; private set; }

    // Use this for initialization
    void Start () {
        Ticker.earlyTickEvent += Fire;
        animator = GetComponentInChildren<Animator>();
	}
	
	void Fire() {
        DashPlayerController target = FindObjectOfType<DashPlayerController>();
        if (target) {
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0;
            transform.forward = dir;
        }

        fired = Instantiate(prefab.gameObject, barrel.position, transform.rotation);
        fired.GetComponent<Bullet>().startPoint = transform.position + transform.forward;
        if (animator) {
            animator.SetTrigger("Fire");
        }
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
