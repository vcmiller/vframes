using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour {
    public static Ticker inst { get; private set; }

    public delegate void Listener();
    public event Listener earlyTickEvent;
    public event Listener tickEvent;
    public event Listener lateTickEvent;

    public float delay;

    public float lastTick { get; private set; }

	// Use this for initialization
	void Awake () {
        inst = this;
        lastTick = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastTick >= delay) {
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
}
