using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Animator animator { get; private set; }
    public float clipTime = 0.25f;
    public bool primed { get; private set; }
    public static int defaultLayerMask { get; private set; }

	void Hit(Vector3 pos) {
        pos.y = 0;
        transform.position = pos;
        GetComponent<PlayerDeathAnim>().OnDeath();
    }

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        defaultLayerMask = LayerMask.GetMask("Default");
    }

    private void Update() {
        if (Ticker.inst.timeUntilTick <= clipTime) {
            if (primed) {
                animator.SetTrigger("AnimPlay");
                primed = false;
            }
        } else {
            primed = true;
        }
    }

    private void OnEnable() {
        Ticker.lateTickEvent += CheckPlatform;
    }

    private void OnDisable() {
        Ticker.lateTickEvent -= CheckPlatform;
    }

    private void CheckPlatform() {
        if (!Physics.Raycast(transform.position + Vector3.up, Vector3.down, 2.0f, defaultLayerMask)) {
            Hit(transform.position);
        }
    }
}
