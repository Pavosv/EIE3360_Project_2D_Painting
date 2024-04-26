using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public string NextScene;
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextScene);
    }
}
