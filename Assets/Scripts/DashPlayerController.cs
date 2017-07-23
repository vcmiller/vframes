﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayerController : PlayerController<ControlProxy> {
    private bool mouseMode = true;
    public int mousePlaneLayerMask { get; private set; }

    public void Axis_MouseX(float value) {
        if (value != 0) {
            mouseMode = true;
        }
    }

    public void Axis_MouseY(float value) {
        if (value != 0) {
            mouseMode = true;
        }
    }

    public void Axis_Horizontal(float value) {
        if (value != 0) {
            mouseMode = false;
        }
    }

    public void Axis_Vertical(float value) {
        if (value != 0) {
            mouseMode = false;
        }
    }

    protected override void Awake() {
        base.Awake();

        mousePlaneLayerMask = LayerMask.GetMask("MouseTarget");
    }

    protected override void Update() {
        base.Update();

        if (mouseMode) {
            UpdateDirectionFromMouse();
        } else {
            UpdateDirectionFromGamepad();
        }
    }

    void UpdateDirectionFromGamepad() {
        Vector3 right = viewTarget.transform.right;
        Vector3 fwd = viewTarget.transform.forward;

        right.y = 0;
        fwd.y = 0;
        right = right.normalized;
        fwd = fwd.normalized;

        Vector3 v = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * fwd;
        
        if (v.sqrMagnitude > 0.4f) {
            v = v.normalized;
            controlled.movementInput = v;
        }
    }

    void UpdateDirectionFromMouse() {

        Ray ray = viewTarget.camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, mousePlaneLayerMask)) {
            Vector3 toHit = hit.point - controlled.transform.position;
            toHit.y = 0;
            toHit = toHit.normalized;

            controlled.movementInput = toHit;
        }
    }
}
