using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using Mirror;

namespace SiegeDefense.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
    public class PlayerMove : NetworkBehaviour
    {
        #region Components
        [SerializeField]
        [Required]
        private Rigidbody2D rb;

        [SerializeField]
        [Required]
        private PlayerInput input;
        #endregion

        #region Serialized fields
        [SerializeField]
        [Range(0, 15)]
        private float speed = 8;

        [SerializeField]
        [Range(0, 8)]
        private float maxVelocChange = 6f;
        #endregion

        private Vector2 _input;

        private void Start()
        {
            if (!isLocalPlayer)
            {
                enabled = false;
                return;
            }
        }

        private void FixedUpdate()
        {
            Vector2 velocChange = (_input * speed) - rb.velocity;

            velocChange.x = Mathf.Clamp(velocChange.x, -maxVelocChange, maxVelocChange);
            velocChange.y = Mathf.Clamp(velocChange.y, -maxVelocChange, maxVelocChange);

            rb.AddForce(velocChange, ForceMode2D.Impulse);
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            _input = ctx.ReadValue<Vector2>().normalized;
        }
    }
}
