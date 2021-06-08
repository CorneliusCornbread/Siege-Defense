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
        private HealthChangeEvent _onHealthChange;

        public HealthChangeEvent OnHealthChange
        {
            get { return _onHealthChange; }
            set { _onHealthChange = value; }
        }

        public void ServerDamage(int damage)
		{
			health -= damage;
			OnHealthChange.Invoke(health);
		}
	}
}