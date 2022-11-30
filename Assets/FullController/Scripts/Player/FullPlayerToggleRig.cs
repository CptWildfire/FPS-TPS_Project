using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace FullController.Scripts.Player
{
    public class FullPlayerToggleRig : MonoBehaviour
    {
        public Rig rig;

        public void Toggle(bool toggle)
        {
            rig.weight = toggle ? 1f : 0f;
        }
    }
}