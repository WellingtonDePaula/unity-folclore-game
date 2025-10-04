using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour {
    [SerializeField] GameObject _solidCollider;
    [SerializeField] GameObject _textObject;
    [SerializeField] string _targetScene;
    bool _playerNearby = false;

    [SerializeField] GameObject _minigame;
    [SerializeField] QuickPressMinigameData _minigameData;
    private void Update() {
        if (_playerNearby) {
            if (PlayerInputReader.Instance.Interact && MinigameManager.Instance.Minigame == null && !MinigameManager.Instance.Victory) {

                MinigameManager.Instance.StartQuickPressMinigame(_minigame, _minigameData);

                PlayerInputReader.Instance.ConsumeInteractInput();
            }
        }
        if(MinigameManager.Instance.Victory && _solidCollider.GetComponent<Collider2D>().enabled) {
            _textObject.SetActive(false);
            _solidCollider.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if(this.gameObject == GameManager.Instance.DoorToOpen) {
                _playerNearby = true;
                if(!MinigameManager.Instance.Victory) {
                    _textObject.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _playerNearby = false;
            _textObject.SetActive(false);
        }
    }
}
