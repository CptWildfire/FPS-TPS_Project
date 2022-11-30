using FullController.Scripts.Player;
using UnityEngine;

namespace FullController.Scripts.Weapons
{
    public class FullPlayerWeapons : MonoBehaviour
    {

        public Transform fpsParent = null;
        public Transform fpsAimParent = null;
        public Transform tpsParent = null;
        public Transform weapon = null;
        
        private FullPlayerView.ViewMod viewMod;

        public void ChangeWeaponView(FullPlayerView.ViewMod mod)
        {
            viewMod = mod;
            if (viewMod == FullPlayerView.ViewMod.Fps)
                SetWeaponFPS();
            if (viewMod == FullPlayerView.ViewMod.Fps_Aim)
                SetWeaponFPSAim();
            if (viewMod == FullPlayerView.ViewMod.Tps)
                SetWeaponTPS();
        }

        private void SetWeaponFPS()
        {
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            weapon.SetParent(fpsParent);
        }
        private void SetWeaponFPSAim()
        {
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            weapon.SetParent(fpsAimParent);
        }
        private void SetWeaponTPS()
        {
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            weapon.SetParent(tpsParent);
        }
    }
}