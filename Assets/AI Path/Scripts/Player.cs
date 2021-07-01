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
        public State state;
        [NonSerialized] public NavMeshAgent agent;
        [SerializeField] GameObject GoalDoor;
        [SerializeField] GameObject GoalUI;
        //NavMeshSurface meshSurface;
        Animator anim;
        OpenDoor currentOpenDoor;
        LineRenderer line;
        [SerializeField] WayPoint collect1;
        [SerializeField] WayPoint collect2;
        [SerializeField] WayPoint collect3;
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
            if(state == State.Collect)
            {
                if (!collectOne && !collectTwo && !collectThree)
                {
                    agent.SetDestination(collect1.Position);
                    if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                    {
                        collectOne = true;
                    }
                }
                if (collectOne && !collectTwo && !collectThree)
                {
                    agent.SetDestination(collect2.Position);
                    if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                    {
                        collectTwo = true;
                    }
                }
                if(collectOne && collectTwo && !collectThree)
                {
                    agent.SetDestination(collect3.Position);
                    if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                    {
                        collectThree = true;
                    }
                }
                if(collectOne && collectTwo && collectThree)
                {
                    GoalDoor.transform.Rotate(Vector3.up, 90);
                    state = State.PathFind;
                }
            }

            if(state == State.PathFind)
            {
                agent.SetDestination(waypoint.Position);
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
            if(state == State.Goal)
            {
                GoalUI.SetActive(true);
                Time.timeScale = 0;
            }
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
            agent.SetDestination(_opendoor.doorSwitch.Position);
            state = State.OpenDoor;
            currentOpenDoor = _opendoor;
        }
    }

    public enum State
    {
        PathFind,
        OpenDoor,
        Collect,
        Goal
    }
}
