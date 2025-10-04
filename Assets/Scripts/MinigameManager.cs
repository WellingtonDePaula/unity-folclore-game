using UnityEngine;

public class MinigameManager : MonoBehaviour {
    [SerializeField] Canvas _canvas;
    GameObject _minigame = null;
    bool _victory = false;

    public bool Victory { get => _victory; set => _victory = value; }
    public GameObject Minigame { get => _minigame; }
    public static MinigameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void StartQuickPressMinigame(GameObject minigameObject, QuickPressMinigameData data) {
        GameManager.Instance.GamePaused = true;
        _minigame = Instantiate(minigameObject, _canvas.transform);
        _minigame.GetComponent<QuickPressBehaviour>().Data = data;
        _minigame.GetComponent<QuickPressBehaviour>().manager = this.gameObject;
    }

    private void Update() {
        
    }

    public void EndMinigame() {
        _minigame = null;
        GameManager.Instance.GamePaused = false;
    }
}
