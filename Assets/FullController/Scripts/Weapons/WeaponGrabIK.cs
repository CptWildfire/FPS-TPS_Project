using System;
using UnityEngine;

namespace FullController.Scripts.Weapons
{
    public class WeaponGrabIK : MonoBehaviour
    {
        public GrabIKData leftGrab;
        public GrabIKData rightGrab;
        
        [Serializable]
        public struct GrabIKData
        {
            public Transform target;
            public Transform hint;
        }
    }
}