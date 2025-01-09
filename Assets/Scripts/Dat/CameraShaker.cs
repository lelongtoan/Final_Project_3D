using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private float shakeTimer;

    private void Start()
    {
        if(virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        if (SceneManager.GetActiveScene().name == "LobbyMap" || SceneManager.GetActiveScene().name == "PathwayMap")
        {
            virtualCamera.m_Lens.NearClipPlane = -100;
        }
        else
        {
            virtualCamera.m_Lens.NearClipPlane = 0.5f; ;
        }
    }
    public void ShakeCamera(float intensity, float time)
    {
        Debug.Log("Shake");
        var perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                var perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
