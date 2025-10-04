using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineKey : MonoBehaviour, ISaveable {
    bool _nearby = false;
    bool _active = true;

    private void Awake() {
        SaveManager.Instance.RegisterSaveable(this);
    }
    private void Update() {
        if(_nearby) {
            if(Input.GetKeyDown(KeyCode.E)) {
                EventsManager.Instance.mineKey = true;
                _active = false;
                SaveObject(SceneManager.GetActiveScene().name);
                Destroy(gameObject);
            }
        }
    }
    public void Deactive() {
        SaveManager.Instance.RemoveSaveable(this);
    }

    public void LoadObject(string sceneName) {
        _active = Convert.ToBoolean(PlayerPrefs.GetInt($"{sceneName}:mineKeyActive", 1));
        EventsManager.Instance.mineKey = Convert.ToBoolean(PlayerPrefs.GetInt($"{sceneName}:mineKeyToUse", 0));
        this.gameObject.SetActive(Convert.ToBoolean(PlayerPrefs.GetInt($"{sceneName}:mineKeyActive", 1)));
    }

    public void SaveObject(string sceneName) {
        Debug.Log(Convert.ToInt16(this.gameObject.activeSelf));
        PlayerPrefs.SetInt($"{sceneName}:mineKeyActive", Convert.ToInt16(_active));
        PlayerPrefs.SetInt($"{sceneName}:mineKeyToUse", Convert.ToInt16(EventsManager.Instance.mineKey));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            _nearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            _nearby = false;
        }
    }

    private void OnDestroy() {
        Deactive();
    }
}
