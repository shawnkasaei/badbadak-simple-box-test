using TMPro;
using UnityEngine;

public class UIFPSCounter : MonoBehaviour
{
    [HideInInspector] public int avgFrameRate;
    
    private TMP_Text display_Text;

    private void Awake() => display_Text = transform.GetComponent<TMP_Text>();

    public void Update()
    {
        float current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        
        display_Text.text =  $"{avgFrameRate} FPS";
    }
}
