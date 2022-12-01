using System;
using FullController.Scripts.Player;
using UnityEngine;

namespace FullController.Scripts.Weapons
{
    public class WeaponTransform : MonoBehaviour
    {
        public WeaponViewTransform tpsTransform;

        [Serializable]
        public struct WeaponViewTransform
        {
            public FullPlayerView.ViewMod view;
            public Vector3 position;
            public Vector3 rotation;
        }
    }
}