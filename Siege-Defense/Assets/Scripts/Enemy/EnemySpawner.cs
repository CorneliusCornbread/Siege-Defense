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

		public bool ScaleByTime = true;

        [Range(0, 2)]
        public float SpawnsPerSecond = 1;

        [Range(0, 1)]
        public float Randomness = .2f;

        public float SecondsPerWave = 1000;

		private float _timeSinceLastSpawn = 0;

        private float _time = 0;

        private void OnEnable()
        {
            if (isServer) return;

            _time = 0;

            _timeSinceLastSpawn = Mathf.Infinity;
        }

        private void FixedUpdate()
        {
            if (!isServer) return;

            _time += Time.fixedDeltaTime;

            float effectiveSpawns = SpawnsPerSecond + (SpawnsPerSecond * Random.Range(0, Randomness));

			if (ScaleByPlayerCount)
            {
				effectiveSpawns *= PlayerCounter.Count;
            }

            if (ScaleByTime)
            {
                effectiveSpawns *= 1 + (_time / SecondsPerWave);
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
            enemy.transform.position = transform.position;
            NetworkServer.Spawn(enemy);
        }
    }
}