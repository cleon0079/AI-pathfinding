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

            pointB = transform.GetChild(0).position.z;
            pointA = transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            speed = Random.Range(0, 5);
            if (transform.position.z <= pointA)
            {
                dir = 1;
            }
            if (transform.position.z >= pointB)
            {
                dir = -1;
            }

            transform.Translate(0, 0, speed * dir * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Agent" && carveIsStationary)
            {
                meshObstacle.carveOnlyStationary = false;
                carveIsStationary = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Agent" && !carveIsStationary)
            {
                meshObstacle.carveOnlyStationary = true;
                carveIsStationary = true;
            }
        }
    }
}
