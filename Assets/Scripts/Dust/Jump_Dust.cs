using UnityEngine;

namespace Dust
{
    public class Jump_Dust : MonoBehaviour
    {
        public void AnimationEnd()
        {
            Destroy(this.gameObject);
        }
    }
}
