using Mirror;
using SiegeDefense.Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiegeDefense.Enemy
{
	[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
	public class EnemyBehaviour : NetworkBehaviour
	{
		[SerializeField]
		[Required]
		private Rigidbody2D rb;

        [SerializeField]
        [Required]
        private SpriteRenderer rend;

        [SerializeField]
        private Vector2 randDirRange;

        [SerializeField]
        private int damageDealt = 10;

		private Vector3 _moveDirection;

        public const string DefWallTag = "Defense Wall";
        public const string ColWallTag = "Collision Wall";

        private void Start()
        {
            if (!isServer)
            {
                rb.simulated = false;
                enabled = false;
                return;
            }

            _moveDirection.x = Random.Range(-randDirRange.x, -1);
            int isStrafer = Random.Range(0, 2);
            _moveDirection.y = Random.Range(-randDirRange.y, randDirRange.y) * isStrafer;
        }

        private void FixedUpdate()
        {
			rb.velocity = _moveDirection;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isServer) return;

            if (collision.gameObject.CompareTag(DefWallTag))
            {
                collision.gameObject.GetComponent<Health>().ServerDamage(damageDealt);
                NetworkServer.Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag(ColWallTag))
            {
                Bounce();
            }
        }

        public void OnHealthChange(int health)
        {
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            NetworkServer.Destroy(gameObject);
        }

        private void Bounce()
        {
            _moveDirection.y *= -1;
        }
    }
}