using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickPressBehaviour : MonoBehaviour {
    PlayerInputReader _inputReader = null;
    [SerializeField] Slider _pressSlider;
    [SerializeField] Slider _timeSlider;
    [SerializeField] float _increaseRate;
    QuickPressMinigameData _data = null;
    bool _stop = false;
    bool _init = false;

    public GameObject manager;
    
    public QuickPressMinigameData Data { get => _data; set => _data = value; }

    private void Awake() {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _inputReader = PlayerInputReader.Instance;
    }

    // Update is called once per frame
    void Update() {
        if(!_init) {
            _timeSlider.maxValue = _data.timeLimit;
            _timeSlider.minValue = 0;
            _timeSlider.value = _data.timeLimit;
            _init = true;
        }
        if(_timeSlider.value <= 0) {
            EndGame();
        }
    }

    private void FixedUpdate() {
        if (_inputReader.MinigamePress && !_stop) {
            Pressed();
            _inputReader.ConsumeMinigamePressInput();
        }
        if (_data != null && !_stop) {
            _timeSlider.value -= Time.fixedDeltaTime;
            _pressSlider.value = Mathf.Clamp(_pressSlider.value - (_data.decreaseRate * Time.fixedDeltaTime), _pressSlider.minValue, _pressSlider.maxValue);
        }
    }

    void Pressed() {
        gameObject.GetComponent<UIShaker>().DoUIShake();
        _pressSlider.value = Mathf.Clamp(_pressSlider.value + (_increaseRate - ((_pressSlider.value / _pressSlider.maxValue) * CheckZero(_increaseRate))) * Time.fixedDeltaTime, _pressSlider.minValue, _pressSlider.maxValue);
        Debug.Log((_increaseRate - ((_pressSlider.value / _pressSlider.maxValue) * CheckZero(_increaseRate))) * Time.fixedDeltaTime);
    }

    float CheckZero(float value) {
        if (value == 0) {
            return 0;
        }
        return 1;
    }

    void EndGame() {
        _stop = true;
        if (_pressSlider.value < _pressSlider.maxValue - 15f) {
            manager.GetComponent<MinigameManager>().Victory = false;
        } else {
            manager.GetComponent<MinigameManager>().Victory = true;
        }
        manager.GetComponent<MinigameManager>().EndMinigame();
        gameObject.GetComponent<UIShaker>().enabled = false;
        Destroy(this.gameObject);
    }
}
