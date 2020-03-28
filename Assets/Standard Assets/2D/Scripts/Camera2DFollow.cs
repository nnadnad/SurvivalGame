using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float yBoundary = 0;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        //for camera clamping
        // public bool YMaxEnabled = false;
        // public float YMaxValue = 0;
        // public bool YMinEnabled = false;
        // public float YMinValue = 0;
        // public bool XMaxEnabled = false;
        // public float XMaxValue = 0;
        // public bool XMinEnabled = false;
        // public float XMinValue = 0;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            //vertical

            // if (YMinEnabled && YMaxEnabled) {
            //     m_LastTargetPosition.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
            // } else if (YMinEnabled) {
            //     m_LastTargetPosition.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
            // } else if (YMaxEnabled) {
            //     m_LastTargetPosition.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);
            // } 


            //horizontal

            // if (XMinEnabled && XMaxEnabled) {
            //     m_LastTargetPosition.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
            // } else if (XMinEnabled) {
            //     m_LastTargetPosition.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
            // } else if (XMaxEnabled) {
            //     m_LastTargetPosition.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);
            // } 



            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            // create camera clamping
            // newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yBoundary, newPos.y), newPos.z);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
