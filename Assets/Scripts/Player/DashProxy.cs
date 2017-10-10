using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashProxy : ControlProxy {

    protected override void Awake() {
        base.Awake();

        RegisterInputChannel("Movement", Vector3.zero, false);
    }

    public Vector3 movement {
        get {
            return GetVector("Movement");
        }
        set {
            SetVector("Movement", value);
        }
    }
}
