using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
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
}
