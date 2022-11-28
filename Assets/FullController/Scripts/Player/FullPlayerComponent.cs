using UnityEngine;

namespace FullController.Scripts.Player
{
    public class FullPlayerComponent : MonoBehaviour
    {
        public bool disableComponent = false;
        public bool initialized { get; private set; }
        
        protected FullPlayer fullPlayer = null;
        
        public virtual FullPlayerComponent Initialize(FullPlayer fullPlayer)
        {
            this.fullPlayer = fullPlayer;
            initialized = true;
            return this;
        }
    }
}