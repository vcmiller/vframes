using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMotor : MonoBehaviour {
    public ControlProxy proxy { get; private set; }
    public CapsuleTrail trail { get; private set; }

    public float dashDistance = 2;

    public Mesh targetMesh;

	// Use this for initialization
	void Start () {
        proxy = GetComponent<ControlProxy>();
        trail = GetComponent<CapsuleTrail>();

        Ticker.inst.tickEvent += OnTick;
    }

    void OnTick() {
        transform.position += proxy.movementInput * dashDistance;
        
        trail.Move();
    }

    private void OnEnable() {
        if (Ticker.inst) {
            Ticker.inst.tickEvent += OnTick;
        }
    }

    private void OnDisable() {
        Ticker.inst.tickEvent -= OnTick;
    }

    private void OnRenderObject() {
        Graphics.DrawMeshNow(targetMesh, Matrix4x4.TRS(transform.position + proxy.movementInput * dashDistance, Quaternion.identity, Vector3.one));
    }
}
