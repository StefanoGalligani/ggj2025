using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTriggerScript : MonoBehaviour
{
    [SerializeField] private bool isBottom;
    bool _finished = false;

    private async void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !_finished) {
            _finished = true;
            Time.timeScale = 0;
            if (isBottom) {
                PlayerPrefs.SetInt("LeftScore", PlayerPrefs.GetInt("LeftScore", 0) + 1);
            } else {
                PlayerPrefs.SetInt("RightScore", PlayerPrefs.GetInt("RightScore", 0) + 1);
            }

            await RestartGame();
        }
    }

    private async Task RestartGame() {
        await Task.Delay(1000);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
