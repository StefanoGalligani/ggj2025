using TMPro;
using UnityEngine;

public class ScoreUIScript : MonoBehaviour
{
    [SerializeField] private bool _isRight;
    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        _text.text = "Score: " + PlayerPrefs.GetInt(_isRight ? "RightScore" : "LeftScore", 0);
    }
}
