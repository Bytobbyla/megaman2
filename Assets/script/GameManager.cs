using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] int player;
    public GameObject[] enemigos;
    int enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WinMenu()
    {
        enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemigos.Length == 0)
        {
            Time.timeScale = 0;
            winMenu.SetActive(true);
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void GameOvermenu()
    {
        player--;
        if (player == 0)
        {
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
        }
    }
   
}
