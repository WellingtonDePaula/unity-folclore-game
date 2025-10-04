using System;
using Unity.VisualScripting;
using UnityEngine;

public class DialogBehaviour : MonoBehaviour, ISaveable {
    [SerializeField] DialogData[] _dialogs;
    int _currentDialogIndex = 0;
    bool _canTalk = true;
    public bool CanTalk { get => _canTalk; set => _canTalk = value; }

    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _messageBoxObj;
    GameObject _messageBox;
    GameObject _player = null;

    bool _playerNearby;

    private void Awake() {
        SaveManager.Instance.RegisterSaveable(this);
    }

    private void Update() {
        if (_currentDialogIndex >= _dialogs.Length) {
            _currentDialogIndex = _dialogs.Length;
        }

        if (!_playerNearby) { return; }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (!_messageBox) {
                GameManager.Instance.GamePaused = true;
                _messageBox = Instantiate(_messageBoxObj, _canvas.transform);
                _messageBox.GetComponent<MessageBoxBehaviour>().StartDialog(_dialogs[_currentDialogIndex]);
            } else {
                switch (_dialogs[_currentDialogIndex].context) {
                    case "UnknowDialog":
                        EventsManager.Instance.warehouseKey = true;
                        break;
                }
                _player.GetComponent<PlayerStateMachine>().CurrentState = _player.GetComponent<PlayerStateMachine>().States.Idle();
                GameManager.Instance.GamePaused = false;
                Destroy(_messageBox);
            }
        }
    }

    public void SaveObject(string sceneName) {
        PlayerPrefs.SetInt($"{sceneName}:{_dialogs[_currentDialogIndex].context}", _currentDialogIndex);
    }

    public void LoadObject(string sceneName) {
        _currentDialogIndex = PlayerPrefs.GetInt($"{sceneName}:{_dialogs[_currentDialogIndex].context}", 0);
    }

    public void Deactive() {
        SaveManager.Instance.RemoveSaveable(this);
    }

    private void OnDestroy() {
        Deactive();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _player = collision.gameObject;
            _playerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _player = null;
            _playerNearby = false;
        }
    }
}
