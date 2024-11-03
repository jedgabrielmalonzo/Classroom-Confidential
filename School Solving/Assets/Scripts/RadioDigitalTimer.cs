using System.Collections;
using UnityEngine;
using TMPro;

public class RadioDigitalTimer : MonoBehaviour
{
    public static RadioDigitalTimer Instance;

    [SerializeField] TextMeshProUGUI timerText;

    private int hours = 7;
    private int minutes = 30;
    private float timeIncrement = 5.0f; // Real seconds between each in-game time increment
    private float timePassed = 0;

    void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= timeIncrement)
        {
            timePassed = 0;
            AdvanceTime();
        }
    }

    void AdvanceTime()
    {
        // Add 10 minutes each time this method is called
        minutes += 10;

        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }

        // Stop the timer at 3:30 PM
        if (hours == 15 && minutes == 30)
        {
            enabled = false; // Disable Update to stop the timer
        }

        // Format and display time with AM/PM
        timerText.text = string.Format("{0:00}:{1:00} {2}", 
                        (hours > 12) ? hours - 12 : hours, minutes, (hours >= 12) ? "PM" : "AM");
    }
}
