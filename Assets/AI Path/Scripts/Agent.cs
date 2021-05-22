using UnityEngine;
using UnityEngine.AI;

namespace CleonAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        // Get the waypoint with drag in the waypoint in to this array
        [SerializeField] WayPoint[] waypoints;
        NavMeshAgent agent;
        Animator anim;

        // The random waypoint that agent is current running to
        WayPoint RandomPoint => waypoints[Random.Range(0, waypoints.Length)];


        // Start is called before the first frame update
        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            anim = gameObject.GetComponent<Animator>();

            // When the game start then give it the frist destination
            agent.SetDestination(RandomPoint.position);
        }

        // Update is called once per frame
        void Update()
        {
            // If the agent reach the waypoint then move to a new one
            if (!agent.pathPending && agent.remainingDistance <= 0.1f)
            {
                agent.SetDestination(RandomPoint.position);
            }

            // If the agent is running on its way then use the Run anim
            if(agent.pathPending)
            {
                anim.SetTrigger("Run");
            }
        }
    }
}
