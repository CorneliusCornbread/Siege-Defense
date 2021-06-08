using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiegeDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
	public class BulletMove : NetworkBehaviour
	{
        [SerializeField]
        [Required]
        private Rigidbody2D rb;

		[SerializeField]
		private float speed = 5;

        [SerializeField]
        private int secondsAlive = 120;

        private void Start()
        {
            if (!isServer)
            {
                enabled = false;
                return;
            }

            Invoke(nameof(Die), secondsAlive);
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(speed, 0);
        }

        private void Die()
        {
            if (!isServer) return;

            NetworkServer.Destroy(gameObject);
        }
    }
}