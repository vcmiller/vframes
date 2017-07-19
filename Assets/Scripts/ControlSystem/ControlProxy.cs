using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlProxy : MonoBehaviour {
    public Vector3 movementInput { get; set; }
    public Quaternion rotation { get; set; }

    public Vector3 lastMovementInput { get; private set; }

    protected virtual void Awake() {
        rotation = Quaternion.identity;
    }

	public void AddMovementInput(Vector3 input) {
        movementInput += input;
    }

    public void AddLocalMovementInput(Vector3 input) {
        movementInput += rotation * input;
    }

    public void AddRotationInput(Quaternion input) {
        rotation = input * rotation;
    }

    public void AddLocalRotationInput(Quaternion input) {
        rotation *= input;
    }

    public virtual void ClearInput() {
        movementInput = Vector3.zero;
    }

    public Vector3 TakeMovementInput() {
        if (movementInput.sqrMagnitude > 1) {
            movementInput = movementInput.normalized;
        }

        lastMovementInput = movementInput;
        movementInput = Vector3.zero;
        return lastMovementInput;
    }
}
