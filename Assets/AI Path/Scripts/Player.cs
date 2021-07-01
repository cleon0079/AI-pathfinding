using UnityEngine;
using UnityEngine.AI;
using System;

namespace CleonAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Player : MonoBehaviour
    {
        // Get the waypoint with drag in the waypoint in to this array
        [SerializeField] WayPoint waypoint;
        // State for the agent
        public State state;
        [NonSerialized] public NavMeshAgent agent;
        [SerializeField] GameObject GoalDoor;
        [SerializeField] GameObject GoalUI;
        //NavMeshSurface meshSurface;
        Animator anim;
        // The door then we are current open
        OpenDoor currentOpenDoor;
        LineRenderer line;

        // Three waypoint the we have to get to open the last door to win
        [SerializeField] WayPoint collect1;
        [SerializeField] WayPoint collect2;
        [SerializeField] WayPoint collect3;

        // Check if we have get the 3 waypoint
        bool collectOne = false;
        bool collectTwo = false;
        bool collectThree = false;
        //float timer = 5;
        // Start is called before the first frame update
        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            anim = gameObject.GetComponent<Animator>();
            //meshSurface = FindObjectOfType<NavMeshSurface>();
            line = GetComponent<LineRenderer>();
            state = State.Collect;
            Time.timeScale = 1;
        }

        // Update is called once per frame
        void Update()
        {
            // If we are getting the collection waypoint
            if(state == State.Collect)
            {
                // If we didnt get any then go to the frist one
                if (!collectOne && !collectTwo && !collectThree)
                {
                    agent.SetDestination(collect1.Position);
                    // If we get it pass in true for the frist one
                    if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                    {
                        collectOne = true;
                    }
                }
                // Same as one
                if (collectOne && !collectTwo && !collectThree)
                {
                    agent.SetDestination(collect2.Position);
                    if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                    {
                        collectTwo = true;
                    }
                }
                // Same as two
                if(collectOne && collectTwo && !collectThree)
                {
                    agent.SetDestination(collect3.Position);
                    if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                    {
                        collectThree = true;
                    }
                }
                // If we get all three waypoints, then move to the goal waypoint and open the last door
                if(collectOne && collectTwo && collectThree)
                {
                    GoalDoor.transform.Rotate(Vector3.up, 90);
                    state = State.PathFind;
                }
            }

            // If we are handing to the goal
            if(state == State.PathFind)
            {
                agent.SetDestination(waypoint.Position);
                // If we are at the goal waypoint, then change the state to goal
                if(!agent.pathPending && agent.remainingDistance <= 0.1f)
                {
                    state = State.Goal;
                }
            }
            // If the agent is running on its way then use the Run anim
            if (agent.pathPending)
            {
                anim.SetTrigger("Run");
            }

            // If we are at the opendoor state then go to the switch and open the door and get back to collect state
            if(state == State.OpenDoor)
            {
                if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                {
                    currentOpenDoor.transform.Rotate(Vector3.up, 90);
                    currentOpenDoor.doorIsOpen = true;
                    currentOpenDoor = null;
                    state = State.Collect;
                }
            }
            // If we get the goal the stop the game and show the win UI
            if(state == State.Goal)
            {
                GoalUI.SetActive(true);
                Time.timeScale = 0;
            }
            // Draw the line to the destination
            if (agent.path.corners.Length > 1)
            {
                line.positionCount = agent.path.corners.Length;
                line.SetPositions(agent.path.corners);
            }
            //if(agent.pathStatus == NavMeshPathStatus.PathInvalid || agent.pathStatus == NavMeshPathStatus.PathPartial)
            //{
            //    meshSurface.BuildNavMesh();
            //    Debug.Log(1);
            //}
            //timer -= Time.deltaTime;
            //if (timer <= 0)
            //{
            //    meshSurface.BuildNavMesh();
            //    Debug.Log(5);
            //    timer = 5;
            //}
        }

        public void OpenDoor(OpenDoor _opendoor)
        {
            // If we are in front of a door and need to open
            // Set the destination to the switch of the door, and change the state to open door
            agent.SetDestination(_opendoor.doorSwitch.Position);
            state = State.OpenDoor;
            currentOpenDoor = _opendoor;
        }
    }

    // State of the agent
    public enum State
    {
        PathFind,
        OpenDoor,
        Collect,
        Goal
    }
}
