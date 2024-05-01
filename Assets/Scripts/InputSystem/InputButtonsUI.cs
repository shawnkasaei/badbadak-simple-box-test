using UnityEngine;

public class InputButtonsUI : MonoBehaviour
{
    public static InputButtonsUI Instance;
    
    [SerializeField] private GameObject inputButtonsCanvas;

    private void Awake() => Instance = this;

    public void Enable() => inputButtonsCanvas.SetActive(true);

    public void Disable() => inputButtonsCanvas.SetActive(false);
}
