using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private CustomInputActions customInputActions;
    private InputButtonsUI inputButtonsUI;
    private PlayerController playerController;
    private AgentController agentController;
    private CameraManager cameraManager;
    private int currentInputSystem;

    public void Start()
    {
        customInputActions = new CustomInputActions();
        inputButtonsUI = InputButtonsUI.Instance;
        playerController = PlayerController.Instance;
        agentController = AgentController.Instance;
        cameraManager = CameraManager.Instance;

        customInputActions.Player.Enable();
        customInputActions.Player.InputSwitch.performed += OnInputSwitchPerformed;
        customInputActions.Player.Movement.performed += playerController.OnMovementPerformed;
        
        currentInputSystem = PlayerPrefs.GetInt("CurrentInputSystem", 1);

        GameManager.Instance.OnGameStart += OnGameStart;
    }

    private void SetInputSystem(int pressedNumber, bool disableCurrent = true)
    {
        if (disableCurrent)
        {
            switch (currentInputSystem) // Disable Current Input System
            {
                case 1: // Mouse and Keyboard
                    DisableKeyboardMouse();
                    break;

                case 2: // For Click to Move
                    DisableClickToMove();
                    break;

                case 3: // For UI Buttons
                    DisableUIButtons();
                    break;
            
#if UNITY_EDITOR
                default:
                    Debug.LogError($"Wrong Input System #{pressedNumber}: pressedNumber should be 1, 2 or 3 only. defaulting to Mouse and Keyboard");
                    EnableKeyboardMouse();
                    return;
#endif
            }
        }

        switch (pressedNumber) // Enable New Input System
        {
            case 1: // Mouse and Keyboard
                EnableKeyboardMouse();
                break;

            case 2: // For Click to Move
                EnableClickToMove();
                break;

            case 3: // For UI Buttons
                EnableUIButtons();
                break;

#if UNITY_EDITOR
            default:
                Debug.LogError($"Wrong Input System #{pressedNumber}: pressedNumber should be 1, 2 or 3 only. defaulting to Mouse and Keyboard");
                EnableKeyboardMouse();
                return;
#endif
        }

        currentInputSystem = pressedNumber;
        PlayerPrefs.SetInt("CurrentInputSystem", pressedNumber);
    }

    private void EnableKeyboardMouse()
    {
        EnablePlayerController();
        CursorSetActive(false);
        cameraManager.SetInputAction(0);
        cameraManager.ChangeCameraTo(CameraTypes.Cameras.FreeLook);
    }

    private void EnableClickToMove()
    {
        EnableAgentController();
        cameraManager.ChangeCameraTo(CameraTypes.Cameras.TopDown);
    }

    private void EnableUIButtons()
    {
        EnablePlayerController();
        inputButtonsUI.Enable();
        cameraManager.SetInputAction(1);
        cameraManager.ChangeCameraTo(CameraTypes.Cameras.FreeLook);
    }

    private void DisableKeyboardMouse()
    {
        DisablePlayerController();
        CursorSetActive(true);
    }

    private void DisableClickToMove() => DisableAgentController();

    private void DisableUIButtons()
    {
        DisablePlayerController();
        inputButtonsUI.Disable();
    }

    private void EnablePlayerController() => playerController.Enable();

    private void EnableAgentController() => agentController.Enable();

    private void DisablePlayerController() => playerController.Disable();

    private void DisableAgentController() => agentController.Disable();

    private void CursorSetActive(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
    
    private void OnInputSwitchPerformed(InputAction.CallbackContext context) => SetInputSystem(Int16.Parse(context.control.name));
    
    private void OnGameStart() => SetInputSystem(currentInputSystem, false);
}