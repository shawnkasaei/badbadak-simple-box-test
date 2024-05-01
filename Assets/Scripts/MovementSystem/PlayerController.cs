using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;

    private Transform cameraTransform;
    private CharacterController characterController;
    private Transform rotationObj;
    private Vector3 inputMovement;
    
    private void Awake()
    {
        Instance = this;
        
        characterController = transform.GetComponent<CharacterController>();
        rotationObj = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
    }
    
    private void Update()
    {
        Vector3 motion = Vector3.zero;
            
        float v = inputMovement.z;
        float h = inputMovement.x;
        
        motion += cameraTransform.forward * v * moveSpeed;
        motion += cameraTransform.right * h * moveSpeed;
        motion += Physics.gravity;
        
        characterController.Move(motion * Time.deltaTime);
        
        if (v == 0 && h == 0) return; // Ignore rotation if no input
        
        motion.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(motion);
        rotationObj.rotation = Quaternion.Slerp(rotationObj.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    public void Enable()
    {
        enabled = true;
        characterController.enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        characterController.enabled = false;
    }
    
    public void OnMovementPerformed(InputAction.CallbackContext context) => inputMovement = context.ReadValue<Vector3>();
}