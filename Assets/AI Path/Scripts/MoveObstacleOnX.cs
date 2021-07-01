using UnityEngine;
using UnityEngine.AI;

namespace CleonAI
{
    public class MoveObstacleOnX : MonoBehaviour
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
            pointB = transform.GetChild(0).position.x;
            pointA = transform.position.x;
        }

        // Update is called once per frame
        void Update()
        {
            speed = Random.Range(0, 5);
            if(transform.position.x <= pointA)
            {
                dir = 1;
            }
            if(transform.position.x >= pointB)
            {
                dir = -1;
            }

            transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Agent" && carveIsStationary)
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
