using UnityEngine;

namespace CleonAI
{
    public class OpenDoor : MonoBehaviour
    {
        // Check if the door is open or not
        bool doorIsOpen = false;

        private void OnTriggerEnter(Collider other)
        {
            // If the agent runs in to the area and the door is not open then open the door
            if (other.gameObject.tag == "Agent" && doorIsOpen == false)
            {
                transform.Rotate(Vector3.up, 90);
                doorIsOpen = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // If the agent runs out the area and the door is opened the close the door
            if (other.gameObject.tag == "Agent" && doorIsOpen == true)
            {
                transform.Rotate(Vector3.up, -90);
                doorIsOpen = false;
            }
        }
    }
}
