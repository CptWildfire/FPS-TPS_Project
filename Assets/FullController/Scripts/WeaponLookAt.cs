using System;
using UnityEngine;

namespace FullController.Scripts
{
    public class WeaponLookAt : MonoBehaviour
    {
        public Transform target;

        private void Update()
        {
            transform.LookAt(target);
        }
    }
}