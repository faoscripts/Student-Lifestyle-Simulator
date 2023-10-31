using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject shopScreen;
    public TMP_Text score;
    public TMP_Text dayTxtHUD;

    void Start(){
        if (!DataSystem.newgame) LoadGameUI();
        Time.timeScale = 1;
        NecesidadController.gameOverEv.AddListener(GameOver);
    }

    void GameOver(){
        Time.timeScale = 0;
        score.text = CicloDiaYNoche.contadorDias + " DÍAS";
        gameOverScreen.SetActive(true);
        ShowCursor();
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void PauseScreen(){
        pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
        if (pauseScreen.activeInHierarchy)
        {
            ShowCursor();
        }else{
            HideCursor();
        }
        
    }

    public void UpdateDayHUD(){
        dayTxtHUD.text = "Día " + CicloDiaYNoche.contadorDias; // update day in HUD
    }

    public void SaveGameUI(){
        DataSystem.SaveGame();
    }

    public void LoadGameUI(){
        FindObjectOfType<DataSystem>().LoadGame();
    }

    public static void ShowCursor(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public static void HideCursor(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
