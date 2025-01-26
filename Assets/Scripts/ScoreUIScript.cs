using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ScoreUIScript : MonoBehaviour
{
    [SerializeField] private bool _isRight;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _timeText;

    private async Task Start()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0) {
            await StartGame();
        }
    }

    public async Task StartGame() {
        _text.text = "Score: " + PlayerPrefs.GetInt(_isRight ? "RightScore" : "LeftScore", 0);
        await Countdown();
    }

    private async Task Countdown() {
        Time.timeScale = 0;
        _timeText.text = "3";
        await Task.Delay(1000);
        _timeText.text = "2";
        await Task.Delay(1000);
        _timeText.text = "1";
        await Task.Delay(1000);
        _timeText.text = "";
        Time.timeScale = 1;
    }
}
