using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace FullController.Scripts.Player
{
    public class FullPlayerView : FullPlayerComponent
    {
        public UnityEvent<ViewMod> onViewChanged = null;
        public UnityEvent<bool> onAim;

        public Camera mainCamera = null;
        public Animator animator = null;
        public Transform aimSphere = null;
        public LayerMask aimLayers = new LayerMask();
        public List<ViewData> views = new List<ViewData>();

        private ViewData currentView = null;

        #region Inputs

        public void InputChangeViewCallback(InputAction.CallbackContext callback)
        {
            if (callback.performed || callback.canceled)
            {
                if (callback.action.IsPressed()) ChangePlayerView();
            }
        }

        public void InputAimCallback(InputAction.CallbackContext callback)
        {
            if (callback.performed || callback.canceled)
            {
                PlayerAim(callback.action.IsPressed());
            }
        }

        #endregion

        public override FullPlayerComponent Initialize(FullPlayer fullPlayer)
        {
            currentView = views.FirstOrDefault(v => v.viewMod == ViewMod.Tps);
            return base.Initialize(fullPlayer);
        }

        private void Update()
        {
            UpdateAimFeedBack();
        }

        private void ChangePlayerView()
        {
            if (currentView.viewMod == ViewMod.Tps)
                SwitchView(ViewMod.Fps);
            else
                SwitchView(ViewMod.Tps);
        }

        private void PlayerAim(bool isAim)
        {
            if (isAim)
            {
                if (currentView.viewMod == ViewMod.Tps)
                {
                    SwitchView(ViewMod.Tps_Aim);
                }

                if (currentView.viewMod == ViewMod.Fps)
                {
                    onViewChanged?.Invoke(ViewMod.Fps_Aim);
                }

                animator.SetLayerWeight(1, 1f);
            }
            else
            {
                if (currentView.viewMod == ViewMod.Tps_Aim)
                {
                    SwitchView(ViewMod.Tps);
                }
                
                if (currentView.viewMod == ViewMod.Fps)
                {
                    onViewChanged?.Invoke(ViewMod.Fps);
                }

                animator.SetLayerWeight(1, 0f);
            }
            
            onAim?.Invoke(isAim);


            fullPlayer.controller.rotateOnMove = !isAim;
        }

        private void SwitchView(ViewMod mod)
        {
            currentView = views.First(v => v.viewMod == mod);

            views.ForEach(v => v.ToggleCamera(false));
            currentView.ToggleCamera(true);
            fullPlayer.controller.ChangeCameraRoot(currentView.cameraRoot);

            onViewChanged?.Invoke(mod);
        }

        private void UpdateAimFeedBack()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, aimLayers))
            {
                aimSphere.transform.position = hit.point;
            }
        }

        [Serializable]
        public class ViewData
        {
            public ViewMod viewMod = ViewMod.Tps;
            public GameObject cameraFollow;
            public GameObject cameraRoot;

            public void ToggleCamera(bool toggle)
            {
                if (cameraFollow)
                    cameraFollow.SetActive(toggle);
            }
        }

        [Serializable]
        public enum ViewMod
        {
            Tps,
            Tps_Aim,
            Fps,
            Fps_Aim
        }
    }
}