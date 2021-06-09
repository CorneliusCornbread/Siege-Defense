using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;
using SiegeDefense.Player;

namespace SiegeDefense.Enemy
{
	public class EnemySpawner : NetworkBehaviour
	{
		[SerializeField]
		[Required]
		private GameObject enemyPrefab;

		public bool ScaleByPlayerCount = true;

		public float SpawnsPerSecond = 2;

		private float _timeSinceLastSpawn = 0;

        private void Start()
        {
			if (!isServer) return;
        }

        private void FixedUpdate()
        {
			float effectiveSpawns = SpawnsPerSecond;

			if (ScaleByPlayerCount)
            {
				effectiveSpawns *= PlayerCounter.Count;
            }

			float delay = 1 / effectiveSpawns;

			if (_timeSinceLastSpawn >= delay)
            {
				Spawn();
                _timeSinceLastSpawn = 0;
            }
            else
            {
                _timeSinceLastSpawn += Time.fixedDeltaTime;
            }
        }

		private void Spawn()
        {
            GameObject enemy = Instantiate(enemyPrefab);
            NetworkServer.Spawn(enemy);
        }
    }
}