using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent skeletonAgent;

    new Camera camera;

    // Start is called before the first frame update
    void Start() => camera = Camera.main;

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the camera where we clicked on the screen to the world
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            skeletonAgent.SetDestination(hit.point);
        }
    }
}
