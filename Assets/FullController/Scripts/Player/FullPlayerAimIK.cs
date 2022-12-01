using System;
using UnityEngine;

namespace FullController.Scripts.Player
{
    public class FullPlayerAimIK : MonoBehaviour
    {
        public Transform target;
        public Transform hint;

        private Transform targetRef;
        private Transform hintRef;
        
        public void SetIkRef(Transform targetRef, Transform hintRef)
        {
            this.targetRef = targetRef;
            this.hintRef = hintRef;
        }

        private void Update()
        {
            if (targetRef && target)
            {
                target.transform.position = targetRef.transform.position;
                target.transform.rotation = targetRef.transform.rotation;
            }

            if (hintRef && hint)
            {
                hint.transform.position = hintRef.transform.position;
                hint.transform.rotation = hintRef.transform.rotation;
            }
        }
    }
}