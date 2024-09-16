using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour // Script làm cho nhân vật xoay theo chuột
{
    [Header("All input for target")]
    [SerializeField] private Transform target; //lấy vị trí nhân vật
    [SerializeField] private float xSpeed = 5.0f;
    [SerializeField] private float ySpeed = 4.0f;
    [SerializeField] private float cameraDistanceX = 1f;
    [SerializeField] private float cameraDistanceY = 1.5f;
    [SerializeField] private float cameraDistanceZ = -3f;

    private float cameraDistanceZMin = -2.0f;
    private float cameraDistanceZMax = -3.8f;

    [SerializeField] private float xMinLimit = -360F;
    [SerializeField] private float xMaxLimit = 360F;
    [SerializeField] private float yMinLimit = -40F; // nhìn lên
    [SerializeField] private float yMaxLimit = 80F; // nhìn xuống
    private Quaternion rotation;
    private float x = 0.0f;
    private float y = 0.0f;

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraZoom();
        ChangeDistanceByScrollWheel();
        LockCameraFloor();
    }
    // Hàm lấy giá trị của chuột
    public void CameraMove()
    {
        x += Input.GetAxis("Mouse X") * xSpeed;
        y -= Input.GetAxis("Mouse Y") * ySpeed;

        x = ClampAngle(x, xMinLimit, xMaxLimit);
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        rotation = Quaternion.Euler(y, x, 0); // đảo x và y để chuột không bị ngược hướng
        Vector3 distanceVector = new Vector3(cameraDistanceX, cameraDistanceY, cameraDistanceZ);
        Vector3 position = rotation * distanceVector + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) { angle += 360F; }
        if (angle > 360F) { angle -= 360F; }
        return Mathf.Clamp(angle, min, max);
    }
    public void CameraZoom()
    {
        if (cameraDistanceZ >= cameraDistanceZMax && cameraDistanceZ <= cameraDistanceZMin)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                cameraDistanceZ += 1;
                if (cameraDistanceZ > cameraDistanceZMin) cameraDistanceZ = cameraDistanceZMin;
            }
            if (Input.GetKey(KeyCode.X))
            {
                cameraDistanceZ -= 1;
                if (cameraDistanceZ < cameraDistanceZMax) cameraDistanceZ = cameraDistanceZMax;
            }
        }
    }
    private void ChangeDistanceByScrollWheel()
    {
        if (cameraDistanceZ >= cameraDistanceZMax && cameraDistanceZ <= cameraDistanceZMin)
        {
            cameraDistanceZ += Input.mouseScrollDelta.y;
            if (cameraDistanceZ >= cameraDistanceZMin)
            {
                cameraDistanceZ = cameraDistanceZMin;
            }
            if (cameraDistanceZ <= cameraDistanceZMax)
            {
                cameraDistanceZ = cameraDistanceZMax;
            }
        }
    }
    private void LockCameraFloor()
    {
        if (y > -60 && y < -12)
        {
            cameraDistanceZ = cameraDistanceZMin;
        }
    }
}
