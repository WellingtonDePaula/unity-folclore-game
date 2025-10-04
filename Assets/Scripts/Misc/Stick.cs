using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stick : MonoBehaviour, ISaveable {
    bool _nearby = false;
    bool _active = true;

    private void Awake() {
        SaveManager.Instance.RegisterSaveable(this);
    }
    private void Update() {
        if (_nearby) {
            if (Input.GetKeyDown(KeyCode.E)) {
                EventsManager.Instance.stick = true;
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
        _active = Convert.ToBoolean(PlayerPrefs.GetInt($"{sceneName}:stickActive", 1));
        EventsManager.Instance.mineKey = Convert.ToBoolean(PlayerPrefs.GetInt($"{sceneName}:stickToUse", 0));
        this.gameObject.SetActive(Convert.ToBoolean(PlayerPrefs.GetInt($"{sceneName}:stickActive", 1)));
    }

    public void SaveObject(string sceneName) {
        Debug.Log(Convert.ToInt16(this.gameObject.activeSelf));
        PlayerPrefs.SetInt($"{sceneName}:stickActive", Convert.ToInt16(_active));
        PlayerPrefs.SetInt($"{sceneName}:stickToUse", Convert.ToInt16(EventsManager.Instance.stick));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _nearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _nearby = false;
        }
    }

    private void OnDestroy() {
        Deactive();
    }
}
