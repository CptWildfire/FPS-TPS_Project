using FullController.Scripts.Player;
using FullController.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace FullController.Scripts.Player
{
    public class FullPlayerWeapons : MonoBehaviour
    {

        public Transform fpsParent = null;
        public Transform fpsAimParent = null;
        public Transform tpsParent = null;
        public Transform weapon = null;
        public FullPlayerAimIK leftHand;
        public FullPlayerAimIK rightHand;
        
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
            
            SetHandsIkRef();
        }
        private void SetWeaponFPSAim()
        {
            weapon.SetParent(fpsAimParent);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;
        }
        private void SetWeaponTPS()
        {
            weapon.SetParent(tpsParent);
            weapon.localPosition = weapon.gameObject.GetComponent<WeaponTransform>().tpsTransform.position;
            weapon.localEulerAngles = weapon.gameObject.GetComponent<WeaponTransform>().tpsTransform.rotation;
        }

        private void SetHandsIkRef()
        {
            WeaponGrabIK grabIK = weapon.gameObject.GetComponent<WeaponGrabIK>();
            
            leftHand.SetIkRef(grabIK.leftGrab.target, grabIK.leftGrab.hint);
            rightHand.SetIkRef(grabIK.rightGrab.target, grabIK.rightGrab.hint);
        }
    }
}