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
            weapon.SetParent(fpsParent);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            
            Debug.Log("set weapon fps");
        }
        private void SetWeaponFPSAim()
        {
            weapon.SetParent(fpsAimParent);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            
            Debug.Log("set weapon fps aim");
        }
        private void SetWeaponTPS()
        {
            weapon.SetParent(tpsParent);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
            
            Debug.Log("set weapon tps");
        }
    }
}