using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	void Hit() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
