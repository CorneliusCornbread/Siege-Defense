using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace SiegeDefense.Game
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class WallBehaviour : NetworkBehaviour
	{
		public static WallBehaviour Instance { get; private set; }

		[SerializeField]
		[Required]
		private SpriteRenderer rend;

        private void Awake()
        {
			Assert.IsNull(Instance);
			Instance = this;
        }

        public void OnWallHealthChange(int health)
        {
			if (health <= 0)
            {
				NetworkManager.singleton.StopHost();
            }
        }


	}
}