using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public void LoadCanteen()
    {
        SceneManager.LoadScene("Canteen");
    }

    public void LoadClinicScene()
    {
        SceneManager.LoadScene("Clinic Scene");
    }

    public void LoadComfortRoom()
    {
        SceneManager.LoadScene("Comfort Room");
    }

    public void LoadLibrary()
    {
        SceneManager.LoadScene("Library");
    }

    public void LoadMainClassroom()
    {
        SceneManager.LoadScene("Main Classroom");
    }

    public void LoadMusicRoom()
    {
        SceneManager.LoadScene("Music Room");
    }

    public void LoadStudyHall()
    {
        SceneManager.LoadScene("Study Hall");
    }

    public void LoadHallway()
    {
        SceneManager.LoadScene("Hallway");
    }
}
