using System;
using UnityEngine;

namespace FullController.Scripts.Player
{
    public class FullPlayer : MonoBehaviour
    {
        public FullPlayerController controller { get; private set; }

        private void Start()
        {
            InitializeComponents();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void InitializeComponents()
        {
            controller ??= GetComponent<FullPlayerController>().Initialize(this) as FullPlayerController;
        }
    }
}