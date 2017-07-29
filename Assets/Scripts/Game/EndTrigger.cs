using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {
    public string nextLevel;

    private void OnTriggerEnter(Collider other) {
        PlayerDeathAnim anim = other.GetComponentInParent<PlayerDeathAnim>();
        if (anim) {
            anim.OnWin();

            Invoke("Next", 1.0f);
        }
    }

    void Next() {
        Progress.inst.MarkComplete(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(nextLevel);
    }
}
