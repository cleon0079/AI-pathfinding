using UnityEngine;
using UnityEngine.AI;

namespace CleonAI
{
    public class MoveObstacleOnZ : MonoBehaviour
    {
        float pointB;
        float pointA;
        int dir = 1;
        float speed;
        NavMeshObstacle meshObstacle;
        bool carveIsStationary = true;

        private void Start()
        {
            meshObstacle = GetComponent<NavMeshObstacle>();
            // Get the position of the point that this cube is going
            pointB = transform.GetChild(0).position.z;
            // The position of the cube when it spawn
            pointA = transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            // Get a random speed for the cube
            speed = Random.Range(0, 5);

            // If it hits the range then change the dir
            if (transform.position.z <= pointA)
            {
                dir = 1;
            }
            if (transform.position.z >= pointB)
            {
                dir = -1;
            }
            // Move the cube
            transform.Translate(0, 0, speed * dir * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            // If the agent walk near the cube then bake the map for the agent every 1s
            if(other.gameObject.tag == "Agent" && carveIsStationary)
            {
                meshObstacle.carveOnlyStationary = false;
                carveIsStationary = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // If the agent walk out of the range then dont bake the map
            if (other.gameObject.tag == "Agent" && !carveIsStationary)
            {
                meshObstacle.carveOnlyStationary = true;
                carveIsStationary = true;
            }
        }
    }
}
