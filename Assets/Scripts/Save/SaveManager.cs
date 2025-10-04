using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
    List<ISaveable> _saveableObjects = new List<ISaveable>();

    public static SaveManager Instance;

    private void Awake() {
        //Debug.Log("Awake do Save Manager");
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && this != Instance) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }

    public void SaveGame() {
        

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        Debug.Log($"Game Saved at room {sceneName}");

        if (sceneName != "Menu") {
            PlayerPrefs.SetString("CurrentScene", sceneName);
        }

        List<ISaveable> saveablesToLoad = new List<ISaveable>(_saveableObjects);

        foreach (ISaveable saveable in saveablesToLoad) {
            saveable.SaveObject(sceneName);
        }
    }

    public void LoadGame() {

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        Debug.Log($"Game Loaded at room {sceneName}");

        List<ISaveable> saveablesToLoad = new List<ISaveable>(_saveableObjects);

        foreach (ISaveable saveable in saveablesToLoad) {
            saveable.LoadObject(sceneName);
        }
    }

    public void QuitGame() {
        SaveGame();
        Application.Quit();
    }

    public void RegisterSaveable(ISaveable obj) {
        if (!_saveableObjects.Contains(obj)) {
            _saveableObjects.Add(obj);
        }
    }
    public void RemoveSaveable(ISaveable obj) {
        if (_saveableObjects.Contains(obj)) {
            _saveableObjects.Remove(obj);
        }
    }

    public void DropSave() {
        Debug.Log("Save Dropped");
        PlayerPrefs.DeleteAll();
    }
}
