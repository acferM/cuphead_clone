using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed;
        
        void Start()
        {
            rigidbody2D.velocity = transform.right * speed;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // bullet collision
            }
        }
    }
}
