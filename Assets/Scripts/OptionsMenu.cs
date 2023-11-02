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
    public TMP_Text txtDayFade;
    public TMP_Text infoTxt;

    void Start(){
        if (!DataSystem.newgame) LoadGameUI();
        Time.timeScale = 1;
        infoTxt.gameObject.SetActive(false);
        NecesidadController.gameOverEv.AddListener(GameOver);
        txtDayFade.gameObject.SetActive(false);
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

    public void SaveGameUI(){
        StartCoroutine(InfoText("Partida guardada", 2));
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

    IEnumerator InfoText(string text, float secondsWait){
        infoTxt.gameObject.SetActive(true);
        infoTxt.text = text;
        yield return new WaitForSeconds(secondsWait);
        infoTxt.gameObject.SetActive(false);
    }

    public void UpdateDay(int newDay){
        CicloDiaYNoche.contadorDias = newDay; // update day counter
        txtDayFade.text = "Día " + newDay; // update day text transition screen
        dayTxtHUD.text = "Día " + newDay; // update day in HUD
    }
}
