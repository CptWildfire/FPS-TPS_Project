using System;
using UnityEngine;

namespace FullController.Scripts.Player
{
    public class FullPlayer : MonoBehaviour
    {
        public FullPlayerController controller { get; private set; }
        public FullPlayerView view { get; private set; }

        private void Start()
        {
            InitializeComponents();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void InitializeComponents()
        {
            controller ??= GetComponent<FullPlayerController>().Initialize(this) as FullPlayerController;
            view ??= GetComponent<FullPlayerView>().Initialize(this) as FullPlayerView;
        }
    }
}