using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] string _scene;
    bool _playerNearby;

    private void Update() {
        if (_playerNearby) {
            if (!PlayerInputReader.Instance.Interact) { return; }
            switch (_scene) {
                case "CucaWarehouse":
                    if (!EventsManager.Instance.warehouseKey) { return; }
                    break;
                case "CucaMines":
                    if (!EventsManager.Instance.mineKey) { return; }
                    break;
            }
            GameManager.Instance.LoadScene(_scene);
            PlayerInputReader.Instance.ConsumeInteractInput();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _playerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _playerNearby = false;
        }
    }
}
