using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public static AgentController Instance;

    private NavMeshAgent agent;
    private Camera camera;
    
    private void Awake()
    {
        Instance = this;
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            } 
        }
    }
    

    public void Enable()
    {
        agent.enabled = true;
        enabled = true;
    }

    public void Disable()
    {
        agent.enabled = false;
        enabled = false;
    }
}
