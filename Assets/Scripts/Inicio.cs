using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inicio : MonoBehaviour
{
    [SerializeField] Button loadBtn;

    void Start(){
        if (!System.IO.File.Exists(Application.dataPath + "/GameDataFile.json")) loadBtn.interactable = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Load()
    {
        DataSystem.newgame = false;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
