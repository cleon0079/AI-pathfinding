using UnityEngine;
using UnityEngine.AI;

namespace CleonAI
{
    public class OpenDoor : MonoBehaviour
    {
        // Check if the door is open or not
        public bool doorIsOpen = false;
        // The switch's position to open the door
        public WayPoint doorSwitch;

        private void Start()
        {
            doorSwitch = GetComponentInChildren<WayPoint>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // If the agent runs in to the area and the door is not open then do the open door function
            if (other.gameObject.tag == "Agent" && doorIsOpen == false)
            {
                Player agent = other.GetComponent<Player>();
                agent.OpenDoor(this);
            }
        }
    }
}
