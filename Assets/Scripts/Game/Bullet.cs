using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public bool primed { get; private set; }
    public float range = 100;
    public bool fired { get; private set; }
    public LineRenderer[] flashes;
    public LineRenderer indicator { get; private set; }
    public LineRenderer trail;
    public float flashSpeed = 1;
    public bool hitTarget { get; private set; }
    public Vector3 startPoint { get; set; }

    // Use this for initialization
    void Awake() {
        Ticker.lateTickEvent += LateTick;
        indicator = GetComponent<LineRenderer>();
        trail.enabled = false;
    }

    private void Update() {
        if (!fired) {
            foreach (LineRenderer flash in flashes) {
                if (flash.transform != transform) {
                    flash.SetPosition(1, flash.GetPosition(1) + Vector3.forward * Time.deltaTime * flashSpeed * Ticker.inst.timeUntilTick);
                    flash.SetPosition(0, flash.GetPosition(0) + Vector3.forward * Time.deltaTime * flashSpeed * Ticker.inst.timeUntilTick * 0.3f);
                }
            }

            indicator.widthMultiplier = .1f * Ticker.inst.timeSinceTickRatio;
        } else {
            foreach (LineRenderer flash in flashes) {
                flash.enabled = false;
            }
            indicator.enabled = false;
            trail.enabled = true;

            if (trail.widthMultiplier > 0 && !hitTarget) {
                trail.widthMultiplier = Mathf.Clamp01(Ticker.inst.timeUntilTickRatio * 0.05f - 0.02f);
            }
        }
    }

    void LateTick() {
        if (primed && !fired) {
            Vector3 newPos = startPoint + transform.forward * range;
            newPos.y = 1;
            Vector3 curPos = startPoint;
            curPos.y = 1;
            RaycastHit hit;
            if (Physics.Linecast(curPos, newPos, out hit) ||
                Physics.Linecast(newPos, curPos, out hit)) {
                Vector3 hitPoint = hit.point;

                var root = hit.transform.root;
                root.SendMessage("Hit", hitPoint);
                hitTarget = root.GetComponent<Player>();
            } else {
                Destroy(gameObject, 0.5f);
            }


            transform.position += transform.forward * range;


            fired = true;
        } else {
            primed = true;
        }
    }

    private void OnDestroy() {
        Ticker.lateTickEvent -= LateTick;
    }

    private void OnTriggerEnter(Collider other) {
        other.SendMessageUpwards("Hit", SendMessageOptions.DontRequireReceiver);
    }
}
