using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Camera mainCamera;
    float shakeAmount = 0;

    private void Awake() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }
    }

    // private void Update() {
    //     if (Input.GetKeyDown (KeyCode.T)) {
    //         shake(0.1f, 0.2f);
    //     }
    // }
    public void shake(float amount, float length) {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void BeginShake() {
        if (shakeAmount > 0) {

            Vector3 camPosition = mainCamera.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;


            camPosition.x += offsetX;
            camPosition.y += offsetY;


            mainCamera.transform.position = camPosition;
        }
    }

    void StopShake() {


        CancelInvoke("BeginShake");
        mainCamera.transform.localPosition = Vector3.zero;




    }








}
