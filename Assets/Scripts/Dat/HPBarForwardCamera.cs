using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarForwardCamera : MonoBehaviour
{
    public GameObject cam;
    Vector3 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cam == null)
        {
            cam = GameObject.FindWithTag("Camerafollow");
        }
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
    private void LateUpdate()
    {
    }
}
