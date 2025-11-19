using System;
using Unity.VisualScripting;
using UnityEngine;

public class DialogBehaviour : MonoBehaviour {
    [SerializeField] DialogData[] _dialogs;
    bool _canTalk = true;
    public bool CanTalk { get => _canTalk; set => _canTalk = value; }

    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _messageBoxObj;
    GameObject _messageBox;
    GameObject _player = null;
 
    bool _playerNearby;

    private void Awake() {

    }
    private void Update() {
        if (EventsManager.Instance.unkownDialogIndex >= _dialogs.Length) {
            EventsManager.Instance.unkownDialogIndex = _dialogs.Length;
        }

        if (!_playerNearby) { return; }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (!_messageBox) {
                GameManager.Instance.GamePaused = true;
                _messageBox = Instantiate(_messageBoxObj, _canvas.transform);
                _messageBox.GetComponent<MessageBoxBehaviour>().StartDialog(_dialogs[EventsManager.Instance.unkownDialogIndex]);
            } else {
                switch (_dialogs[EventsManager.Instance.unkownDialogIndex].context) {
                    case "UnknowDialog01":
                        EventsManager.Instance.warehouseKey = true;
                        break;
                    case "UnknowDialog02":
                        EventsManager.Instance.mineKey = true;
                        break;
                    case "UnknowDialog03":
                        EventsManager.Instance.canCreateSlingshot = true;
                        break;
                }
                _player.GetComponent<PlayerStateMachine>().CurrentState = _player.GetComponent<PlayerStateMachine>().States.Idle();
                GameManager.Instance.GamePaused = false;
                Destroy(_messageBox);
            }
        }
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
