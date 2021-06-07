using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiegeDefense.Enemy
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyBehaviour : NetworkBehaviour
	{
		[SerializeField]
		[Required]
		private Rigidbody2D rb;

		public Vector3 moveDirection;

        private void FixedUpdate()
        {
			rb.velocity = moveDirection;
        }
    }
}