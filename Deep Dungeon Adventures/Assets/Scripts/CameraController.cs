using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
    private bool isShaking = false;
    public CinemachineVirtualCamera vCam;
    
    public void CameraShake()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCam());
        }
    }

    IEnumerator ShakeCam()
    {
        isShaking = true;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 2;
        yield return new WaitForSeconds(0.10f);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        isShaking = false;

    }
}
