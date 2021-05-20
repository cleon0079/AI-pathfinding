using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    float originRotation;

    private void Awake()
    {
        originRotation = transform.rotation.y;
    }

    private void Update()
    {
        if(transform.rotation.y >= originRotation && transform.rotation.y <= originRotation + 90)
        {

        }
    }
}
