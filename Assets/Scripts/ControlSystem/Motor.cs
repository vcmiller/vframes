using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControlProxy))]
public class Motor<T> : MonoBehaviour where T : ControlProxy {
    public T control { get; private set; }

    protected virtual void Awake() {
        control = GetComponentInParent<T>();
    }

    protected virtual void Update() {
        DoMovement(control.TakeMovementInput());
    }

    protected virtual void DoMovement(Vector3 input) {

    }
}
