using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject _continueOption;

    private void Start() {
        if(PlayerPrefs.GetString("CurrentScene", "Menu") == "Menu") {
            _continueOption.SetActive(false);
        } else {
            _continueOption.SetActive(true);
        }
    }
    public void ContinueGame() {
        Debug.Log(PlayerPrefs.GetString("CurrentScene"));
        GameManager.Instance.LoadScene(PlayerPrefs.GetString("CurrentScene"));
    }

    public void NewGame() {
        SaveManager.Instance.DropSave();
        SceneManager.LoadScene("CucaJail");
    }
 
    public void QuitGame() {
        SaveManager.Instance.QuitGame();
    }
}
