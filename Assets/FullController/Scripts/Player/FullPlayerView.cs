using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FullController.Scripts.Player
{
    public class FullPlayerView : FullPlayerComponent
    {
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
                if (callback.action.IsPressed()) PlayerAim();
            }
        }

        #endregion

        public override FullPlayerComponent Initialize(FullPlayer fullPlayer)
        {
            currentView = views.FirstOrDefault(v => v.viewMod == ViewMod.Tps);
            return base.Initialize(fullPlayer);
            
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
        
        private void PlayerAim()
        {
            
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