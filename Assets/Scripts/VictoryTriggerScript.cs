using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTriggerScript : MonoBehaviour
{
    [SerializeField] private bool isBottom;
    // [SerializeField] private TextMeshProUGUI _topText;
    // [SerializeField] private TextMeshProUGUI _bottomText;
    // bool _finished = false;

    [SerializeField] private VictoryManagerScript _victoryManager;

    bool _finished = false;

    // private void Start()
    // {
    //     _topText.enabled = false;
    //     _bottomText.enabled = false;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !_finished)
        {
            _finished = true;
            _victoryManager.EndGame(isBottom);

            // _topText.enabled = true;
            // _bottomText.enabled = true;

            // _topText.text = isBottom ? "You Won!" : "You Lost!";
            // _bottomText.text = isBottom ? "You Lost!" : "You Won!";

            // Time.timeScale = 0;
            // if (isBottom)
            // {
            //     PlayerPrefs.SetInt("LeftScore", PlayerPrefs.GetInt("LeftScore", 0) + 1);
            // }
            // else
            // {
            //     PlayerPrefs.SetInt("RightScore", PlayerPrefs.GetInt("RightScore", 0) + 1);
            // }

            // await RestartGame();
        }
    }

    // private async Task RestartGame()
    // {
    //     await Task.Delay(1000);
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(1);
    // }
}
