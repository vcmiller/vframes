using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingPlatform : MonoBehaviour {
    public Image[] images;

    [HideInInspector]
    private int _activeIndex = -1;

    public Vector3 nextPos { get; private set; }
    public int moveOnStart;

    public int activeIndex {
        get {
            return _activeIndex;
        }

        set {
            if (_activeIndex >= 0) {
                images[_activeIndex].enabled = false;
            }

            _activeIndex = value;

            if (_activeIndex >= 0) {
                images[_activeIndex].enabled = true;
            }
        }
    }

    protected virtual void UpdateImageByVector(Vector3 dir) {
        if (dir.sqrMagnitude > 0) {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.z);
            int index = (Mathf.RoundToInt(angle / 45) % 8 + 8) % 8;
            activeIndex = index;
        } else {
            activeIndex = -1;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
        nextPos = transform.position;
        foreach (Image img in images) {
            img.enabled = false;
        }

        LateTick();

        for (int i = 0; i < moveOnStart; i++) {
            print("HI");
            EarlyTick();
            LateTick();
        }
	}
	
	protected virtual void LateTick() {
        nextPos = GetComponent<MovingPlatformController>().GetNextPos();
        UpdateImageByVector(nextPos - transform.position);
    }

    protected virtual void EarlyTick() {
        transform.position = nextPos;
    }

    private void OnEnable() {
        Ticker.lateTickEvent += LateTick;
        Ticker.earlyTickEvent += EarlyTick;
    }

    private void OnDisable() {
        Ticker.lateTickEvent -= LateTick;
        Ticker.earlyTickEvent -= EarlyTick;
    }
}
