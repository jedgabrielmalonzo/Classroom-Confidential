using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
 public void Pause()
 {
    pauseMenu.SetActive(true);
    Time.timeScale = 0;
 }
 public void Resume()
 {
    pauseMenu.SetActive(false);
    Time.timeScale = 1;
 }
 public void Home()
 {
    SceneManager.LoadScene("Main Screen");
    Time.timeScale = 1;
 }
 public void Minimap()
 {
    SceneManager.LoadScene("Hallway");
    Time.timeScale = 1;
 }
}
