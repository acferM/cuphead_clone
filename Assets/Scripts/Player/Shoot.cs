using UnityEngine;

namespace Player
{
    public class Shoot : MonoBehaviour
    {
        private PlayerControls _controls;

        [Header("Components")] 
        [SerializeField] private Animator animator;
        
        [Header("Bullet")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private float bulletCooldown;
        private float _lastBullet;
        private bool _isShooting;

        private void Awake()
        {
            _controls = new PlayerControls();

            _controls.Attack.Shoot.performed += ctx =>
            {
                animator.SetBool("Shooting", true);
                _isShooting = ctx.ReadValue<bool>();
            };

            _controls.Attack.Shoot.canceled += ctx =>
            {
                animator.SetBool("Shooting", false);
                _isShooting = ctx.ReadValue<bool>();
            };
        }

        private void Update()
        {
            if (!_isShooting || !(Time.time > _lastBullet + bulletCooldown)) return;
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            _lastBullet = Time.time;
        }

        private void OnEnable()
        {
            _controls.Enable();
        }
        
        private void OnDisable()
        {
            _controls.Disable();
        }
    }
}
