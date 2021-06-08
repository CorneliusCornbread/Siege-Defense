using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace SiegeDefense.Game
{
	[System.Serializable]
	public class HealthChangeEvent : UnityEvent<int> { }

	public class Health : NetworkBehaviour
	{
		[SerializeField]
		[SyncVar]
		private int health = 100;

		[SerializeField]
		private bool visualHealthAffect = true;

		[SerializeField]
		private SpriteRenderer rend;

		[SerializeField]
        private HealthChangeEvent _onHealthChange;

        public HealthChangeEvent OnHealthChange
        {
            get { return _onHealthChange; }
            set { _onHealthChange = value; }
        }

        public void ServerDamage(int damage)
		{
			if (!isServer) return;

			health -= damage;
			UpdateSprite(health);
			OnHealthChange.Invoke(health);
		}

		private void UpdateSprite(int health)
		{
			if (!visualHealthAffect) return;

			Color c = rend.color;
			c.a = Mathf.Clamp(health / 100f, 0.2f, 1f);
			rend.color = c;
		}
	}
}