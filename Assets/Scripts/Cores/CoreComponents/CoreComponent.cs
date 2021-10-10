using Interfaces;
using UnityEngine;

namespace Cores.CoreComponents
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core Core;

        protected virtual void Awake()
        {
            Core = transform.parent.GetComponent<Core>();

            if (Core == null)
            {
                Debug.LogError("There is no core on the parent");
            }

            Core.AddComponent(this); // Add the component to the list
        }

        public virtual void LogicUpdate()
        {
        }
    }
}