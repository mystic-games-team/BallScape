using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    static public CameraEffects get;

    CinemachineVirtualCamera CBC;

    private void Awake()
    {
        get = this;

        CBC = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CBC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += intensity;
        StartCoroutine(StopShake(intensity, time, cinemachineBasicMultiChannelPerlin));
    }

    IEnumerator StopShake(float intensity, float time, CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin)
    {
        yield return new WaitForSeconds(time);

        basicMultiChannelPerlin.m_AmplitudeGain -= intensity;
    }
}
