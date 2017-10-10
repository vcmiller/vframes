using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Ticker : MonoBehaviour {
    public static Ticker inst { get; private set; }
    public static float tickMultiplier = 1;

    public delegate void Listener();
    public static event Listener earlyTickEvent;
    public static event Listener tickEvent;
    public static event Listener lateTickEvent;

    public float delay;

    public float lastTick { get; private set; }

    public float timeUntilTick {
        get {
            return actualDelay - timeSinceTick;
        }
    }

    public float timeSinceTick {
        get {
            return Mathf.Clamp(Time.time - lastTick, 0, actualDelay);
        }
    }

    public float timeUntilTickRatio {
        get {
            return 1.0f - timeSinceTickRatio;
        }
    }

    public float timeSinceTickRatio {
        get {
            return timeSinceTick / actualDelay;
        }
    }

    public float actualDelay {
        get {
            return delay / tickMultiplier;
        }
    }

	// Use this for initialization
	void Awake () {
        lastTick = Time.time;
        inst = this;
	}

	
	// Update is called once per frame
	void Update () {
		if (timeSinceTick >= actualDelay) {
            print("TICKER TICK " + gameObject.name);

            lastTick = Time.time;

            if (earlyTickEvent != null) {
                earlyTickEvent();
            }

            if (tickEvent != null) {
                tickEvent();
            }

            if (lateTickEvent != null) {
                lateTickEvent();
            }
        }
	}

    private void OnDestroy() {
        earlyTickEvent = null;
        tickEvent = null;
        lateTickEvent = null;
    }
}
