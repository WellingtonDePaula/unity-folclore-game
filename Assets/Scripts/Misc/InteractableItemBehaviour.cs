using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableItemBehaviour : MonoBehaviour, ISaveable {
    [SerializeField] NoteData _data;
    [SerializeField] GameObject _textObject;
    [SerializeField] Canvas _noteCanvas;
    [SerializeField] GameObject _noteObject;

    [SerializeField] GameObject _targetObject = null;
    [SerializeField] bool _instaOpen = false;
    bool _active = true;

    bool _playerNearby = false;

    private void Awake() {
        SaveManager.Instance.RegisterSaveable(this);
    }

    private void Update() {
        if (_playerNearby) {
            if (PlayerInputReader.Instance.Interact) {
                GameObject note = Instantiate(_noteObject, _noteCanvas.transform);
                note.GetComponent<NoteBehaviour>().Data = _data;
                note.GetComponent<NoteBehaviour>().RefreshData();
                PlayerInputReader.Instance.ConsumeInteractInput();

                if (_targetObject != null) {
                    if (!_instaOpen) {
                        GameManager.Instance.DoorToOpen = _targetObject;
                    } else {
                        _targetObject.GetComponent<Collider2D>().enabled = false;
                    }
                }
                _active = false;
                SaveObject(SceneManager.GetActiveScene().name);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _playerNearby = true;
            _textObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _playerNearby = false;
            _textObject.SetActive(false);
        }
    }

    public void SaveObject(string sceneName) {
        PlayerPrefs.SetInt($"{sceneName}:{_data.noteId}", Convert.ToInt32(_active));
        Debug.Log($"{sceneName}:{_data.noteId}: " + Convert.ToInt32(_active));
    }

    public void LoadObject(string sceneName) {
        int value = PlayerPrefs.GetInt($"{sceneName}:{_data.noteId}", 1);

        Debug.Log($"{sceneName}:{_data.noteId}: " + value);

        if (value <= 0) {
            Deactive();
        }
        gameObject.SetActive(Convert.ToBoolean(value));

    }

    public void Deactive() {
        SaveManager.Instance.RemoveSaveable(this);
    }

    private void OnDestroy() {
        Deactive();
    }
}
