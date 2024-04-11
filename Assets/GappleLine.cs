using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GappleLine : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

   
    void Update()
    {
        line.SetPosition(0, transform.position);
    }

    public void StartGrapple(Vector3 point)
    {
        line.SetPosition(1,point);
        line.enabled = true;
    }

    public void EndGrapple()
    {
        line.enabled = false;
    }
}
