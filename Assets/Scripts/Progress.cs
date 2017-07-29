using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Progress : MonoBehaviour {
    public LevelsCompleted completed { get; private set; }

    public string saveFile = "game.sav";

    public string savePath {
        get {
            return Application.persistentDataPath + "/" + saveFile;
        }
    }

    public static Progress inst { get; private set; }
    public BinaryFormatter formatter { get; private set; }

    private void Awake() {
        inst = this;
        formatter = new BinaryFormatter();

        if (!Load()) {
            completed = new LevelsCompleted();
            completed.completed = new bool[SceneManager.sceneCountInBuildSettings - 1];
        }
        
    }

    public void MarkComplete(int level) {
        completed.completed[level - 1] = true;
        Save();
    }

    public void Delete() {
        File.Delete(savePath);
    }

    void Save() {
        FileStream file = File.Create(savePath);
        formatter.Serialize(file, completed);
        file.Close();
    }

    bool Load() {
        if (File.Exists(savePath)) {
            try {
                FileStream file = File.Open(savePath, FileMode.Open);
                completed = (LevelsCompleted)formatter.Deserialize(file);
                file.Close();
                return true;
            } catch (IOException) {
                return false;
            }
        } else {
            return false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
