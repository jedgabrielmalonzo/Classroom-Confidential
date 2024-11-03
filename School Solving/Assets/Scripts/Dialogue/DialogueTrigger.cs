using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
        if (visualCue != null)
        {
            visualCue.SetActive(false);
        }

         DontDestroyOnLoad(visualCue);
        
        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetDialogueTrigger(); // Call your reset method
    }

    private void ResetDialogueTrigger()
    {
        playerInRange = false; // Reset playerInRange to false
        if (visualCue != null)
        {
            visualCue.SetActive(false); // Ensure the visual cue is hidden
        }
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (visualCue != null)
            {
                visualCue.SetActive(true);
            }

            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager dialogueManager = DialogueManager.GetInstance();
                if (dialogueManager != null)
                {
                    dialogueManager.EnterDialogueMode(inkJSON);
                }
                else
                {
                    Debug.LogError("DialogueManager instance is null!");
                }
            }
        }
        else
        {
            if (visualCue != null)
            {
                visualCue.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area");
            playerInRange = false;
        }   
    }
}
