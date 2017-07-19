using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayerController : PlayerController<ControlProxy> {

    public void Axis_MouseX(float value) {
        if (value != 0) {
            UpdateDirectionFromMouse();
        }
    }

    public void Axis_MouseY(float value) {
        if (value != 0) {
            UpdateDirectionFromMouse();
        }
    }

    public void Axis_Horizontal(float value) {
        if (value != 0) {
            UpdateDirectionFromGamepad();
        }
    }

    public void Axis_Vertical(float value) {
        if (value != 0) {
            UpdateDirectionFromGamepad();
        }
    }

    void UpdateDirectionFromGamepad() {
        Vector3 v = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if (v.sqrMagnitude > 0.4f) {
            v = v.normalized;
            controlled.movementInput = v;
        }
    }

    void UpdateDirectionFromMouse() {
        Ray ray = viewTarget.camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Vector3 toHit = hit.point - controlled.transform.position;
            toHit.y = 0;
            toHit = toHit.normalized;

            controlled.movementInput = toHit;
        }
    }
}
