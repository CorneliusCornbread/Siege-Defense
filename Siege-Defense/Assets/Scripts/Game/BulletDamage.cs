using UnityEngine;
using Mirror;

namespace SiegeDefense.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
	public class BulletDamage : NetworkBehaviour
	{
		[SerializeField]
		private int damage = 10;

        [SerializeField]
        private string targetTag;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isServer) return;

            if (collision.gameObject.CompareTag(targetTag))
            {
                collision.gameObject.GetComponent<Health>().ServerDamage(damage);
            }

            NetworkServer.Destroy(gameObject);
        }
    }
}