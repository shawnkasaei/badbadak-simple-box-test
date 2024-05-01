using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public static AgentController Instance;

    private NavMeshAgent agent;
    private Camera mainCamera;
    private Transform rotationObj;
    
    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        rotationObj = transform.GetChild(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            } 
        }
    }

    public void Enable()
    {
        rotationObj.rotation = new Quaternion(0,0,0,1);
        agent.enabled = true;
        enabled = true;
    }

    public void Disable()
    {
        agent.enabled = false;
        enabled = false;
    }
}
