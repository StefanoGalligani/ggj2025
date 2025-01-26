using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManagerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _topText;
    [SerializeField] private TextMeshProUGUI _bottomText;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private float _timerSec = 60;

    private float _startTime;

    private void Start()
    {
        _topText.enabled = false;
        _bottomText.enabled = false;

        _startTime = Time.time;
    }

    private void Update()
    {
        float deltaTime = Time.time - _startTime;
        _timerText.text = Math.Ceiling(_timerSec - deltaTime).ToString();

        if (deltaTime >= _timerSec)
        {
            EndGame(false);
        }
    }

    public async void EndGame(bool isBottom)
    {
        _topText.enabled = true;
        _bottomText.enabled = true;

        _topText.text = isBottom ? "You Won!" : "You Lost!";
        _bottomText.text = isBottom ? "You Lost!" : "You Won!";

        Time.timeScale = 0;
        if (isBottom)
        {
            PlayerPrefs.SetInt("LeftScore", PlayerPrefs.GetInt("LeftScore", 0) + 1);
        }
        else
        {
            PlayerPrefs.SetInt("RightScore", PlayerPrefs.GetInt("RightScore", 0) + 1);
        }

        await RestartGame();
    }

    private async Task RestartGame()
    {
        _startTime = Time.time;

        await Task.Delay(1000);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}