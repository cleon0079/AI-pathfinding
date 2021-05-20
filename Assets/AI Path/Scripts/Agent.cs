using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        [SerializeField] WayPoint[] waypoints;
        NavMeshAgent agent;
        Animator anim;

        WayPoint RandomPoint => waypoints[Random.Range(0, waypoints.Length)];


        // Start is called before the first frame update
        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            anim = gameObject.GetComponent<Animator>();
            agent.SetDestination(RandomPoint.position);
        }

        // Update is called once per frame
        void Update()
        {
            if (!agent.pathPending && agent.remainingDistance <= 0.1f)
            {
                agent.SetDestination(RandomPoint.position);
            }

            if(agent.pathPending)
            {
                anim.SetTrigger("Run");
            }
        }
    }
}
