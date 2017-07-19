using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(ControlProxy))]
public class AIController : MonoBehaviour {
    public StateMachineDefinition stateMachineDefinition;

    public StateMachineRunner stateMachine { get; private set; }

    protected virtual void Awake() {
        if (stateMachineDefinition == null) {
            Debug.LogWarning("Warning: No state machine specified.");
        } else {
            stateMachine = new StateMachineRunner(gameObject);
            stateMachine.Run(stateMachineDefinition);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (stateMachineDefinition != stateMachine.definition) {
            stateMachine.Run(stateMachineDefinition);
        }

        stateMachine.Update();
	}
}
