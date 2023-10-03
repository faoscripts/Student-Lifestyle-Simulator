using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TMP_Text score;

    void Start(){
        NecesidadController.gameOverEv.AddListener(GameOver);
    }

    void GameOver(){
        // Time.timeScale = 0;
        score.text = CicloDiaYNoche.contadorDias + " DAYS";
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RestartGame(){
        print("enter RestartGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMainMenu(){
        print("enter ReturnMainMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame(){
        print("enter ExitGame");
        Application.Quit();
    }
}
