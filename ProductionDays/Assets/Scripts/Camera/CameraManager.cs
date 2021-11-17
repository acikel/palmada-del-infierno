using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    private Vector3 originalPosition;

    private float shakeDuration = 0;
    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    private Coroutine shakeRoutine;
    
    void Start()
    {
        InstanceRepository.Instance.AddOnce(this);
    }

    private void OnDestroy()
    {
        InstanceRepository.Instance.Remove(this);
    }

    public void ScreenShake(float shakeDuration, float shakeAmount = 0.3f, float decreaseFactor = 1.0f)
    {
        originalPosition = transform.localPosition;

        this.shakeDuration += shakeDuration;
        this.shakeAmount = shakeAmount;
        this.decreaseFactor = decreaseFactor;

        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(DoShake());
        }
    }

    private IEnumerator DoShake()
    {
        while (shakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime;
            shakeAmount -= Time.deltaTime * decreaseFactor;

            yield return null;
        }

        shakeDuration = 0;
        transform.localPosition = originalPosition;
        shakeRoutine = null;
    }
}
