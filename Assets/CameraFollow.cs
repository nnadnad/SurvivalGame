﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Transform player;
    Vector3 velocity = Vector3.zero;

    float nextTimeToSearch = 0;

    public float smoothTime = .15f;

        //for camera clamping
        public bool YMaxEnabled = false;
        public float YMaxValue = 0;
        public bool YMinEnabled = false;
        public float YMinValue = 0;
        public bool XMaxEnabled = false;
        public float XMaxValue = 0;
        public bool XMinEnabled = false;
        public float XMinValue = 0;



    private void FixedUpdate() {

        if (target == null) {
            FindPlayer();

            // player = Transform.Find("playerPrefabs");
            return;
            // target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // get target position
        Vector3 targetPos = target.position;

            //vertical

            if (YMinEnabled && YMaxEnabled) {
                targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
            } else if (YMinEnabled) {
                targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
            } else if (YMaxEnabled) {
                targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);
            } 

            //horizontal

            if (XMinEnabled && XMaxEnabled) {
                targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
            } else if (XMinEnabled) {
                targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
            } else if (XMaxEnabled) {
                targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);
            } 


        // align the camera and target position z
        targetPos.z = transform.position.z;

        // using smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    void FindPlayer () {
        if (nextTimeToSearch <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
            if (searchResult != null)
                target = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}