using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public float defaultLength = 10f;
    public GameObject dot;

    private LineRenderer lR = null;

    private void Awake()
    {
        lR = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        //Use default value for length
        float targetLength = defaultLength;
        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);
        //Default end
        Vector3 endPosition = transform.position + (transform.forward * targetLength);
        //Update position if hit
        if(hit.collider != null)
        {
            endPosition = hit.point;
        }
        //Set position of dot
        dot.transform.position = endPosition;
        //Set position of line renderer
        lR.SetPosition(0, transform.position);
        lR.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);
        return hit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pointer")
        {

        }
    }
}
