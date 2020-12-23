using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
   [SerializeField] private int currScene;
    [SerializeField] private float gameOverDelay = 3f;
    public void LoadNextLevel()
    {
        currScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currScene + 1);
        GameManager.currlevel = currScene + 1;
    }
    public void LoadPreviousLevel()
    {
        currScene = GameManager.currlevel;

        SceneManager.LoadScene(currScene );
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadGameOver());
       

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOver");
    }
}
