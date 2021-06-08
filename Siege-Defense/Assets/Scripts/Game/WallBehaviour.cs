using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiegeDefense.Game
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class WallBehaviour : NetworkBehaviour
	{
		[SerializeField]
		[Required]
		private SpriteRenderer rend;

		public void OnWallHealthChange(int health)
        {
			if (health <= 0)
            {
				Debug.Log("Game over");
            }

			UpdateSprite(health);
        }

		private void UpdateSprite(int health)
        {
			Color c = rend.color;
			c.a = health / 100f;
			rend.color = c;
        }
	}
}