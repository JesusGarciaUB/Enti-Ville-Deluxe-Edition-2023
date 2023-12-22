using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    private TextMeshProUGUI textmesh;
    private Color ogColor;
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject question;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private bool IsImage;
    private void Awake()
    {
        if (!IsImage)
        {
            textmesh = GetComponent<TextMeshProUGUI>();
            ogColor = textmesh.color;
        }
    }

    public void MouseEnter()
    {
        textmesh.color = Color.grey;
    }

    public void MouseExit()
    {
        textmesh.color = ogColor; 
    }

    public void DeleteSave()
    {
        Database._DATABASE.DeleteGame();
        NextScene();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void AskQuestion()
    {
        mainmenu.SetActive(false);
        question.SetActive(true);
    }

    public void Refuse()
    {
        mainmenu.SetActive(true);
        question.SetActive(false);
    }

    public void Save()
    {
        Database._DATABASE.SaveCurrentGame();
    }

    public void SaveExit()
    {
        Save();
        ExitApp();
    }

    public void PauseMenu()
    {
        pausemenu.SetActive(!pausemenu.activeSelf);
    }
}
