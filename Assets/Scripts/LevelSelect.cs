using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public GameObject buttonPrefab;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Progress.inst.completed.completed.Length; i++) {
            if (i == 0 || Progress.inst.completed.completed[i - 1]) {
                GameObject obj = Instantiate(buttonPrefab);

                var t = obj.GetComponent<RectTransform>();
                t.SetParent(transform, false);

                var b = obj.GetComponent<Button>();
                var text = obj.GetComponentInChildren<Text>();
                text.text = "LEVEL " + (i + 1);

                int cpy = i;

                b.onClick.AddListener(new UnityEngine.Events.UnityAction(()=> {
                    SceneManager.LoadScene(cpy + 1);
                }));
            }
        }
	}
}
