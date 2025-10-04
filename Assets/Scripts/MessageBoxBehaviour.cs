using UnityEngine;
using UnityEngine.UI;

public class MessageBoxBehaviour : MonoBehaviour {
    //DialogData _dialog;
    [SerializeField] Text _txtDialog;

    public void StartDialog(DialogData dialog) {
        _txtDialog.text = dialog.text;
    }

}
