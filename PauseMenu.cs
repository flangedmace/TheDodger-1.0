using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _lostPanel;
    [SerializeField] private string _mainMenu;

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        _enemyGenerator.enabled = false;
        Time.timeScale = 0f;
    }
    
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        _enemyGenerator.enabled = true;
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(_mainMenu);
    }
}
