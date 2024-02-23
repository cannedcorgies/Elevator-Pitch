using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine("WaitForSec");
    }
    IEnumerator WaitForSec(){
        yield return new WaitForSeconds(3.4f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }
}
