using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPotision()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
    }

    public void BackToGame()
    {
        PauseMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        
    }
}
