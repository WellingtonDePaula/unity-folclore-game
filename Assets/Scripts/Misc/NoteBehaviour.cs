using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehaviour : MonoBehaviour {
    [SerializeField] NoteData _data;
    [SerializeField] TMP_Text _contentText;
    [SerializeField] Image _backgroundImage;

    public NoteData Data { get => _data; set => _data = value; }

    private void Awake() {
        Time.timeScale = 0;
    }

    private void Update() {
        if(PlayerInputReader.Instance.Interact) {
            CloseNote();
            PlayerInputReader.Instance.ConsumeInteractInput();
        }
    }

    public void RefreshData() {
        _backgroundImage.sprite = _data.background;
        _contentText.text = _data.content;
        _backgroundImage.enabled = true;
        _contentText.enabled = true;
    }

    void CloseNote() {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
