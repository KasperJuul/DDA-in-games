using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void StaticGame()
    {
        SceneManager.LoadScene(1);
    }

    public void DynamicGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Death()
    {
        StartCoroutine(WaitAfterDeath());
    }

    IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
