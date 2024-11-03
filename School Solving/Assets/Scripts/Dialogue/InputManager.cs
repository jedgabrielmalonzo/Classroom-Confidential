using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private bool interactPressed = false;
    private bool submitPressed = false;

    public static Vector2 Movement { get; private set; } // Static property for movement

    private static InputManager instance;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _submitAction;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep instance across scenes
        }

        _playerInput = GetComponent<PlayerInput>();
        InitializeInputActions();

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        // Subscribe to the Submit action
        if (_submitAction != null)
        {
            _submitAction.performed += SubmitButtonPressed;
            _submitAction.canceled += SubmitButtonCanceled;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (_submitAction != null)
        {
            _submitAction.performed -= SubmitButtonPressed;
            _submitAction.canceled -= SubmitButtonCanceled;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
    }

    public static InputManager GetInstance() 
    {
        return instance;
    }

    private void InitializeInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _submitAction = _playerInput.actions["Submit"];

        // Assign event handlers to submit action
        _submitAction.performed += SubmitButtonPressed;
        _submitAction.canceled += SubmitButtonCanceled;
        
        Debug.Log("Input actions initialized.");
    }

    // Method to reinitialize input actions after scene change
    public void ReinitializeInputActions()
    {
        InitializeInputActions();
        Debug.Log("Input actions reinitialized.");
    }

    // Scene loaded callback
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReinitializeInputActions();
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
            Debug.Log("Interact button pressed"); // Log for debugging
        }
        else if (context.canceled)
        {
            interactPressed = false;
        } 
    }

    public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    // Method for submit action press
    private void SubmitButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
            Debug.Log("Submit button pressed"); // Log for debugging
        }
    }

    private void SubmitButtonCanceled(InputAction.CallbackContext context)
    {
        submitPressed = false; // Reset when released
    }

    // Method to check if submit was pressed
    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }
}
