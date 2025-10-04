using Unity.Cinemachine;
using UnityEngine;

public class CameraMagnet : MonoBehaviour {
    [SerializeField] GameObject _magnetPosition;
    [SerializeField] bool _magnetized = false;

    public GameObject MagnetPosition { get { return _magnetPosition; } set { _magnetPosition = value; } }
    public bool Magnetized { get => _magnetized; set => _magnetized = value; }

    void Start() {
        if(_magnetized) {
            CameraManager.Instance.GetComponent<CinemachineCamera>().Follow = _magnetPosition.transform;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if(collision.CompareTag("Player")) {
    //        if (collision.CompareTag("Player")) {
    //            if (!_magnetized) {
    //                Camera.main.GetComponent<CameraBehaviour>().TargetObject = _magnetPosition;
    //                _magnetized = true;
    //            } else {
    //                Camera.main.GetComponent<CameraBehaviour>().TargetObject = Camera.main.GetComponent<CameraBehaviour>().PlayerObject;
    //                _magnetized = false;
    //            }
    //        }
    //    }
    //}
}
