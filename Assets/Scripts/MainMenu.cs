using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public float increment;
    public Text multiplierDisplay;

    private string defaultText;

    private void Start() {
        defaultText = multiplierDisplay.text;
    }

    private void Update() {
        multiplierDisplay.text = defaultText + " (current: x" + Ticker.tickMultiplier + ")";
    }

    public void Play(string level) {
        SceneManager.LoadScene(level);
    }

    public void GitGud() {
        Ticker.tickMultiplier += increment;
    }

    public void ISuckAtVideoGamesAndPrettyMuchEverythingElseToo() {
        Ticker.tickMultiplier = 1;
    }
}
