using System;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject _leftTutorialPanel;
    [SerializeField] private GameObject _rightTutorialPanel;
    private int _currentLeftPanel = 0;
    private int _currentRightPanel = 0;
    private void Start() {
        if (PlayerPrefs.GetInt("tutorial") == 1) {
            Time.timeScale = 0;
            ActivateFirstChild(_leftTutorialPanel);
            ActivateFirstChild(_rightTutorialPanel);
        } else {
            _leftTutorialPanel.SetActive(false);
            _rightTutorialPanel.SetActive(false);
            Destroy(this);
        }
    }

    private void ActivateFirstChild(GameObject panel) {
        panel.SetActive(true);
        foreach (Transform child in panel.transform) {
            child.gameObject.SetActive(false);
        }
        panel.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            NextPanelLeft();
        }
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
            NextPanelRight();
        }
    }

    private void NextPanelLeft() {
        if (_currentLeftPanel < _leftTutorialPanel.transform.childCount) {
            _currentLeftPanel++;
        }
        try {
            _leftTutorialPanel.transform.GetChild(_currentLeftPanel - 1).gameObject.SetActive(false);
        } catch(Exception e) {}
        if (_currentLeftPanel == _leftTutorialPanel.transform.childCount) {
            if (_currentRightPanel == _rightTutorialPanel.transform.childCount) {
                EndTutorial();
            }
        } else {
            _leftTutorialPanel.transform.GetChild(_currentLeftPanel).gameObject.SetActive(true);
        }
    }

    private void NextPanelRight() {
        if (_currentRightPanel < _rightTutorialPanel.transform.childCount) {
            _currentRightPanel++;
        }
        _rightTutorialPanel.transform.GetChild(_currentRightPanel - 1).gameObject.SetActive(false);
        if (_currentRightPanel == _rightTutorialPanel.transform.childCount) {
            if (_currentLeftPanel == _leftTutorialPanel.transform.childCount) {
                EndTutorial();
            }
        } else {
            _rightTutorialPanel.transform.GetChild(_currentRightPanel).gameObject.SetActive(true);
        }
    }

    private void EndTutorial() {
        PlayerPrefs.SetInt("tutorial", 0);
        _leftTutorialPanel.SetActive(false);
        _rightTutorialPanel.SetActive(false);
        _ = _leftTutorialPanel.GetComponentInParent<ScoreUIScript>().StartGame();
        _ = _rightTutorialPanel.GetComponentInParent<ScoreUIScript>().StartGame();
        Destroy(this);
    }
}
