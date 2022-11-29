using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FullController.Scripts.Player
{
    public class FullPlayerView : FullPlayerComponent
    {
        public Camera mainCamera = null;
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
                currentView = views.First(v => v.viewMod == ViewMod.Fps);
            else
                currentView = views.First(v => v.viewMod == ViewMod.Tps);

            views.ForEach(v => v.ToggleCamera(false));
            currentView.ToggleCamera(true);
            fullPlayer.controller.ChangeCameraRoot(currentView.cameraRoot);
        }
        private void PlayerAim(bool isAim)
        {
            if (isAim)
            {
                currentView = views.First(v => v.viewMod == ViewMod.Tps_Aim);

                views.ForEach(v => v.ToggleCamera(false));
                currentView.ToggleCamera(true);
                fullPlayer.controller.ChangeCameraRoot(currentView.cameraRoot);
            }
            else
            {
                currentView = views.First(v => v.viewMod == ViewMod.Tps);

                views.ForEach(v => v.ToggleCamera(false));
                currentView.ToggleCamera(true);
                fullPlayer.controller.ChangeCameraRoot(currentView.cameraRoot);
            }
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
                if(cameraFollow) 
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