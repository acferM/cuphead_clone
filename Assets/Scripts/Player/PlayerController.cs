using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private Animator animator;
        
        [Header("Movement")]
        [SerializeField] private float speed;
        private float _moveDirection;
        private PlayerControls _controls;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private GameObject jumpDust;
        private bool _onGround = true;

        private void Awake()
        {
            _controls = new PlayerControls();

            _controls.Movement.Walk.performed += ctx =>
            {
                var input = ctx.ReadValue<float>();
                transform.localScale = new Vector3(input >= 0 ? 1 : -1, 1);
                _moveDirection = input;
            };
            _controls.Movement.Walk.canceled += _ => _moveDirection = 0;

            _controls.Movement.Jump.performed += _ => Jump();
        }

        private void Update()
        {
            rigidbody.velocity = new Vector3(Mathf.Round(_moveDirection) * speed, rigidbody.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(_moveDirection));
        }
        
        private void Jump() 
        {
            if (!_onGround) return;
                
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
            _onGround = false;
            Instantiate(jumpDust, transform.position, Quaternion.identity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("floor")) return;
            
            _onGround = true;
            animator.SetBool("Jumping", false);
        }

        private void OnEnable()
        {
            _controls.Movement.Enable();
        }

        private void OnDisable()
        {
            _controls.Movement.Disable();
        }
    }
}
