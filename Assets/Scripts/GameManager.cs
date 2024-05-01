using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public delegate void GameStart();
    public GameStart OnGameStart;

    [SerializeField] private GameObject startMessage;

    private void Awake() => Instance = this;

    public void StartGame()
    {
        startMessage.SetActive(false);
        OnGameStart();
    }
}
