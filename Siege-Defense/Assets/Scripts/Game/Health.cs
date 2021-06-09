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
		[SyncVar(hook = nameof(NewHealth))]
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

		public bool IsFullHealth => health >= StartHealth;

		public int StartHealth { get; private set; }

        private void Start()
        {
			StartHealth = health;
        }

        public void ServerDamage(int damage)
		{
			if (!isServer) return;

			health -= damage;
			UpdateSprite(health);
			OnHealthChange.Invoke(health);
		}

		public void ServerSetHealth(int newHealth)
        {
			health = newHealth;
			UpdateSprite(health);
			OnHealthChange.Invoke(health);
		}

		public void ServerResetHealth()
        {
			ServerSetHealth(StartHealth);
		}

		private void UpdateSprite(int health)
		{
			if (!visualHealthAffect) return;

			Color c = rend.color;
			c.a = Mathf.Lerp(.5f, 1, health / 100f);
			rend.color = c;
		}

		private void NewHealth(int newHealth, int oldHealth)
        {
			UpdateSprite(newHealth);
        }
	}
}