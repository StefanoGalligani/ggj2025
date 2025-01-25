using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] Toggle _tutorialToggle;
    private void Awake() {
        _creditsPanel.SetActive(false);
        _tutorialToggle.isOn = PlayerPrefs.GetInt("tutorial", 1) == 1;
        
        PlayerPrefs.SetInt("LeftScore", 0);
        PlayerPrefs.SetInt("RightScore", 0);
    }
    public void OnStartPressed() {
        SceneManager.LoadScene(1);
    }
    public void OnCreditsPressed() {
        _creditsPanel.SetActive(true);
    }
    public void CloseCredits() {
        _creditsPanel.SetActive(false);
    }
    public void OnQuitPressed() {
        Application.Quit();
    }

    public void OnTutorialToggle() {
        PlayerPrefs.SetInt("tutorial", _tutorialToggle.isOn? 1 : 0);
    }

}
