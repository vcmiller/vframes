using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMotor : MonoBehaviour {
    public ControlProxy proxy { get; private set; }
    public CapsuleTrail trail { get; private set; }

    public float dashDistance = 2;
    public float rotationSpeed = 360;

    public Transform directionIndicator;
    public Transform mesh;
    public ParticleSystem dashParticlePrefab;

    private Quaternion meshRotation;

    // Use this for initialization
    void Start () {
        proxy = GetComponent<ControlProxy>();
        trail = GetComponent<CapsuleTrail>();

        meshRotation = mesh.rotation;
    }

    private Vector3 inputSnapped() {
        Vector3 v = proxy.movementInput;
        v.x = Mathf.Round(v.x);
        v.z = Mathf.Round(v.z);
        return v;
    }

    private void Update() {
        directionIndicator.position = transform.position + inputSnapped() * dashDistance + Vector3.up;
        directionIndicator.transform.rotation = Quaternion.LookRotation(Vector3.up, inputSnapped());
    }

    private void LateUpdate() {
        meshRotation = Quaternion.RotateTowards(meshRotation, Quaternion.LookRotation(Vector3.up, inputSnapped()), rotationSpeed * Time.deltaTime * Ticker.inst.timeSinceTickRatio);
        mesh.rotation = meshRotation;
    }

    void OnTick() {
        Vector3 oldPos = transform.position;
        transform.position += inputSnapped() * dashDistance;

        GameObject particles = Instantiate(dashParticlePrefab.gameObject);
        particles.transform.position = (oldPos + transform.position) / 2;
        particles.transform.rotation = Quaternion.LookRotation(Vector3.Cross(inputSnapped(), Vector3.up), Vector3.up);
        Destroy(particles, 5);

        trail.Move();
    }

    private void OnEnable() {
        Ticker.tickEvent += OnTick;
    }

    private void OnDisable() {
        Ticker.tickEvent -= OnTick;
    }
}
