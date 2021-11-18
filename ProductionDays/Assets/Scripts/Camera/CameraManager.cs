using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    private Vector3 originalPosition;

    private float maxShakeDuration = 0;
    private float shakeDuration = 0;
    private float shakeAmount = 0.7f;

    private Coroutine shakeRoutine;
    
    void Awake()
    {
        InstanceRepository.Instance.AddOnce(this);
    }

    private void OnDestroy()
    {
        InstanceRepository.Instance.Remove(this);
    }

    public void ScreenShake(float shakeDuration, float shakeAmount = 0.3f)
    {
        originalPosition = transform.localPosition;

        this.maxShakeDuration = shakeDuration;
        this.shakeDuration += shakeDuration;
        this.shakeAmount = shakeAmount;


        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(DoShake());
        }
    }

    private IEnumerator DoShake()
    {
        float shakeAmountValue = shakeAmount;
        
        while (shakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmountValue;

            shakeDuration -= Time.deltaTime;

            float shakeValue = Mathf.Clamp01(shakeDuration / maxShakeDuration);
            shakeAmountValue = shakeValue * shakeAmount;

            yield return null;
        }

        shakeDuration = 0;
        transform.localPosition = originalPosition;
        shakeRoutine = null;
    }
}
