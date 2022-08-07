using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRaycast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    RaycastHit hitInfo;
    public float hitLength;

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitInfo, hitLength, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitInfo.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitLength, Color.green);
        }
    }
}
