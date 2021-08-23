using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject winCanvas;
    public GameObject loseCanvas;

    private void Start() {
        Time.timeScale = 1;
    }

    private void Update() {
        if(Input.GetKey(KeyCode.Escape))
            LoadScene("MainMenu");
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextLevel() {
        string currentLevelName = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(currentLevelName.Split('_')[1]);
        int nextLevel = currentLevel + 1;
        string nextLevelName = "Level_" + nextLevel.ToString();
        SceneManager.LoadScene(nextLevelName);
    }

    public void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowWinCanvas() {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
    }

    public void ShowLoseCanvas(string loseInfo) {
        Time.timeScale = 0;
        loseCanvas.transform.Find("HintText").gameObject.SetActive(false);
        loseCanvas.SetActive(true);
        loseCanvas.transform.Find("LoseInfo").GetComponent<Text>().text = loseInfo;
    }

    public void ShowHint() {
        loseCanvas.transform.Find("HintText").gameObject.SetActive(true);
    }
}
