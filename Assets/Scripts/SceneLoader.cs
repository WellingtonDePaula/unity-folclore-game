using System;
using UnityEngine;

public class SceneLoader : MonoBehaviour {
    public Action OnSceneLoaded;
    
    private void Awake() {
        Debug.Log("Scene Loader");
        SaveManager.Instance.LoadGame();
        SaveManager.Instance.SaveGame();
        if (OnSceneLoaded != null) {
            OnSceneLoaded();
        }
    }
}
