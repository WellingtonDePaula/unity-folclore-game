using System;
using UnityEngine;

public class EventsManager : MonoBehaviour, ISaveable {
    public bool mineKey = false;
    public bool jailKey = false;
    public bool warehouseKey = false;

    public bool stick = false;

    public static EventsManager Instance { get; private set; }

    private void OnDestroy() {
        Deactive();
    }
    public void Deactive() {
        SaveManager.Instance.RemoveSaveable(this);
    }

    public void LoadObject(string sceneName) {
        mineKey = Convert.ToBoolean(PlayerPrefs.GetInt("mineKey", 0));
        jailKey = Convert.ToBoolean(PlayerPrefs.GetInt("jailKey", 0));
        warehouseKey = Convert.ToBoolean(PlayerPrefs.GetInt("warehouseKey", 0));
        stick = Convert.ToBoolean(PlayerPrefs.GetInt("stick", 0));
    }

    public void SaveObject(string sceneName) {
        PlayerPrefs.SetInt("mineKey", Convert.ToInt16(mineKey));
        PlayerPrefs.SetInt("jailKey", Convert.ToInt16(jailKey));
        PlayerPrefs.SetInt("warehouseKey", Convert.ToInt16(warehouseKey));
        PlayerPrefs.SetInt("stick", Convert.ToInt16(stick));
    }

    private void Awake() {
        SaveManager.Instance.RegisterSaveable(this);
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }
}
