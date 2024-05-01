using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Virtual Cameras:")]
    [SerializeField] private CinemachineVirtualCamera startCamera;
    [SerializeField] private CinemachineFreeLook FreeLookCamera;
    [SerializeField] private CinemachineVirtualCamera TopDownCamera;

    [Space, Header("FreeLook Camera Input Action References")]
    [SerializeField] private InputActionReference inputActionLookArrows;
    [SerializeField] private InputActionReference inputActionLookDelta;

    private CinemachineInputProvider cinemachineInputProvider;
    private CameraTypes.Cameras currentCamera = CameraTypes.Cameras.StartCamera;
    
    private void Awake()
    {
        Instance = this;

        cinemachineInputProvider = FreeLookCamera.GetComponent<CinemachineInputProvider>();
    }

    public void ChangeCameraTo(CameraTypes.Cameras camera)
    {
        if (currentCamera == camera) return;

        switch (currentCamera) // Disable current virtual camera
        {
            case CameraTypes.Cameras.StartCamera:
                startCamera.Priority = 0;
                break;
            
            case CameraTypes.Cameras.FreeLook:
                FreeLookCamera.Priority = 0;
                break;
            
            case CameraTypes.Cameras.TopDown:
                TopDownCamera.Priority = 0;
                break;
            
#if UNITY_EDITOR
            default:
                throw new ArgumentOutOfRangeException(nameof(camera), camera, null);
#endif
        }
        
        switch (camera) // Enable new virtual camera
        {
            case CameraTypes.Cameras.StartCamera:
                startCamera.Priority = 1;
                break;
            
            case CameraTypes.Cameras.FreeLook:
                FreeLookCamera.Priority = 1;
                break;

            case CameraTypes.Cameras.TopDown:
                TopDownCamera.Priority = 1;
                break;
            
#if UNITY_EDITOR
            default:
                throw new ArgumentOutOfRangeException(nameof(camera), camera, null);
#endif
        }

        currentCamera = camera;
    }
    
    /// <summary>
    /// Changes Virtual Camera Input Action
    /// </summary>
    /// <param name="index">Available Options: 0 to use pointer delta, 1 to use Arrows [connected to ui buttons]</param>
    public void SetInputAction(int index)
    {
        switch (index)
        {
            case 0:
                cinemachineInputProvider.XYAxis = inputActionLookDelta;
                break;
            
            case 1:
                cinemachineInputProvider.XYAxis = inputActionLookArrows;
                break;
#if UNITY_EDITOR
            default:
                throw new ArgumentOutOfRangeException();
#endif
        }
    }
}
