using Mono.Cecil.Cil;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    GameObject _doorToOpen;
    bool _gamePaused = false;
    public bool GamePaused { get => _gamePaused; set => _gamePaused = value; }

    public GameObject DoorToOpen { get => _doorToOpen; set => _doorToOpen = value; }
    public static GameManager Instance { get; private set; }
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }

    private void Update() {
        //if(Input.GetKeyDown(KeyCode.R)) {
        //    SaveManager.Instance.DropSave();
        //}
        //if(Input.GetKeyDown(KeyCode.T)) {
        //    SaveManager.Instance.SaveGame();
        //}
    }

    public void LoadScene(string scene) {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(scene);
    }
}
