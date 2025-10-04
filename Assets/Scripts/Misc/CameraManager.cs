using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] float _camSpeed;

    [SerializeField] GameObject _playerObject;
    GameObject _targetObject;
    public float CamSpeed { get { return _camSpeed; } set { _camSpeed = value; } }

    public static CameraManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        _targetObject = _playerObject;


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (gameObject.GetComponent<CinemachineCamera>().Follow == null) {
            gameObject.GetComponent<CinemachineCamera>().Follow = _targetObject.transform;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {

    }
}
