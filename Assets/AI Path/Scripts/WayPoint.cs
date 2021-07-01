using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleonAI
{
    public class WayPoint : MonoBehaviour
    {
        // Get the position of the waypoint and save to use
        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            // Draw out the way point in the scene view
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}
