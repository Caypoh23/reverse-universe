using UnityEngine;

namespace Particles
{
    public class ParticleController : MonoBehaviour
    {
        private void FinishAnimation()
        {
            gameObject.SetActive(false);
        }
    }
}