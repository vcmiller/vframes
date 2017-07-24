using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class PlayerDeathAnim : MonoBehaviour {
    public bool gameOver { get; private set; }

    public SkinnedMeshRenderer renderer;
    public Material deadMaterial;
    public Material winMaterial;
    public Camera camera;

    public float newOrthoSize;
    public float orthoSizeRate;

    public void OnWin() {
        if (!gameOver) {
            gameOver = true;
            Ticker.inst.enabled = false;
            renderer.material = winMaterial;

            GetComponent<DashMotor>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
    }

	public void OnDeath() {
        if (!gameOver) {
            gameOver = true;
            Ticker.inst.enabled = false;
            renderer.material = deadMaterial;

            Invoke("Reload", 1.0f);

            GetComponent<DashMotor>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
    }

    private void Start() {
    }

    private void Update() {
        if (gameOver) {
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, newOrthoSize, orthoSizeRate * Time.deltaTime);
        }
    }

    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
