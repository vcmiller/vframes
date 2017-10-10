﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Motor class for a humanoid character. Enables movement and collision without the use of a Rigidbody.
 * However, if you want to get trigger/collision events for objects that don't have a Rigidbody, you
 * need to put a Rigidbody on this object and make it kinematic.
 */
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMotor : BasicMotor<CharacterProxy> {
    public CapsuleCollider capsule { get; private set; }

    public bool grounded { get; private set; }

    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    private bool wasInAir = false;

    [Header("General")]
    [Tooltip("Increase this if your character is passing through colliders. Should be about 0.05 * the character's height.")]
    public float queryExtraDistance = 0.1f;

    [Tooltip("Layers that block the character.")]
    public LayerMask[] blockingLayers = new LayerMask[1];

    [Header("Movement: Walking")]
    [Tooltip("The max walk speed of the character.")]
    public float walkSpeed = 5;

    [Tooltip("The walking (ground) acceleration of the character.")]
    public float walkAcceleration = 5;

    [Tooltip("The maximum slope, in degrees, that the character can climb.")]
    public float maxSlope = 45;

    [Tooltip("The maximum height that the character can step up onto.")]
    public float maxStep = 0.2f;

    [Header("Jumping")]
    [Tooltip("The speed at which the character jumps.")]
    public float jumpSpeed = 4;

    [Tooltip("The value to multiply Physics.Gravity by.")]
    public float gravityScale = 1;

    [Header("Movement: Falling")]
    [Tooltip("Air control multiplier (air acceleration is Air Control * Walk Acceleration.")]
    public float airControl = 0.5f;

    protected override void Awake() {
        base.Awake();

        capsule = GetComponent<CapsuleCollider>();
    }

    public override void TakeInput() {
        capsule.enabled = false;

        int queryMask = 0;
        foreach (LayerMask lm in blockingLayers) {
            queryMask |= lm.value;
        }

        Vector3 move = control.movement;
        move.y = 0;
        move *= walkSpeed;

        //if (body.isGrounded) {
        float accel = walkAcceleration;
        if (!grounded) {
            accel *= airControl;
        }
        velocity = Vector3.MoveTowards(velocity, new Vector3(move.x, velocity.y, move.z), accel * Time.deltaTime);
        velocity += Physics.gravity * gravityScale * Time.deltaTime;

        if (grounded && control.jump) {
            velocity.y = jumpSpeed;
        }

        RaycastHit hit;

        Vector3 point1, point2;
        float radius, height;
        capsule.GetPoints(out point1, out point2, out radius, out height);

        Vector3 movement = velocity * Time.deltaTime;
        movement = Vector3.ProjectOnPlane(movement, transform.up);
        Vector3 moveDir = movement.normalized;
        float d = movement.magnitude;

        if (d > 0 && Physics.CapsuleCast(point1 - moveDir * queryExtraDistance, point2 - moveDir * queryExtraDistance, radius, movement, out hit, d + queryExtraDistance, queryMask, QueryTriggerInteraction.Ignore)) {
            Vector3 norm = hit.normal;
            float slope = Vector3.Angle(hit.normal, transform.up);

            bool slopeOK = true;
            
            if (slope > maxSlope) {
                norm = Vector3.ProjectOnPlane(norm, transform.up).normalized;
                slopeOK = false;

            } // Else climb slope
            
            // Try to go up step
            RaycastHit stepHit;
            float stepHeight = (maxStep + queryExtraDistance);
            Vector3 stepTestOff = movement + transform.up * stepHeight;

            bool step = false;

            if (!slopeOK) {
                if (Physics.BoxCast((point1 + point2) / 2 + stepTestOff, new Vector3(radius, height / 2, radius), -transform.up, out stepHit, capsule.transform.rotation, stepHeight, queryMask, QueryTriggerInteraction.Ignore)) {
                //if (Physics.CapsuleCast(point1 + stepTestOff, point2 + stepTestOff, radius, -transform.up, out stepHit, stepHeight)) {
                    stepHeight -= stepHit.distance;

                    Vector3 stepHeightOff = transform.up * (stepHeight + queryExtraDistance) - moveDir * queryExtraDistance;

                    if (stepHeight <= maxStep) {
                        slope = Vector3.Angle(stepHit.normal, transform.up);

                        if (slope < maxSlope && !Physics.CapsuleCast(point1 + stepHeightOff, point2 + stepHeightOff, radius, movement, d + queryExtraDistance, queryMask, QueryTriggerInteraction.Ignore)) {
                            step = true;
                            movement += transform.up * stepHeight;
                        }
                    }
                }
            }

            if (!step) {
                float dRem = (d + queryExtraDistance) - hit.distance;
                Vector3 badMovement = movement.normalized * dRem;
                Vector3 comp = Vector3.Project(-badMovement, norm);

                movement += comp;

                comp.y = 0;

                velocity += comp / Time.deltaTime;
            }
        }

        transform.Translate(movement, Space.World);

        Vector3 vert = velocity * Time.deltaTime;
        vert = Vector3.Project(vert, transform.up);
        d = vert.magnitude;
        Vector3 dir = vert.normalized;
        grounded = false;

        if (d > 0 && Physics.CapsuleCast(point1 - dir * queryExtraDistance, point2 - dir * queryExtraDistance, radius, vert, out hit, d + queryExtraDistance, queryMask, QueryTriggerInteraction.Ignore)) {
            Vector3 norm = hit.normal;

            float slope = Vector3.Angle(hit.normal, transform.up);

            if (slope < maxSlope) {
                norm = Vector3.Project(norm, transform.up).normalized;
                grounded = true;
            }

            float dRem = (d + queryExtraDistance) - hit.distance;
            Vector3 badMovement = vert.normalized * dRem;
            Vector3 comp = Vector3.Project(-badMovement, norm);

            vert += comp;

            //comp.x = comp.z = 0;

            if (velocity.y + comp.y > 0) {
                comp.y = Mathf.Max(-velocity.y, 0);
            }

            velocity += comp / Time.deltaTime;
        }

        transform.Translate(vert, Space.World);

        capsule.enabled = true;

        //wasInAir = false;
        /*} else {
            velocity = Vector3.MoveTowards(velocity, new Vector3(move.x, velocity.y, move.z), walkAcceleration * airControl * Time.deltaTime);

            velocity += Physics.gravity * Time.deltaTime;

            wasInAir = true;
        }

        var flags = body.Move(velocity * Time.deltaTime);*/

    }
}
