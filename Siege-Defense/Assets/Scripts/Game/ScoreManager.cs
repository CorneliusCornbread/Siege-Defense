using Mirror;
using SiegeDefense.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace SiegeDefense.Game
{
	public class ScoreManager : NetworkBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField]
        private int wallRepairCost = 200;

        [SerializeField]
        private int pointsPerEnemy = 10;

        public int Score => _score;

        [SyncVar]
        private int _score;

        private Health _wallHealth;

        public Health WallHealth
        {
            get 
            { 
                if (_wallHealth == null)
                {
                    _wallHealth = WallBehaviour.Instance.GetComponent<Health>();
                }
                return _wallHealth;
            }
        }

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }

        private void OnDisable()
        {
            _score = 0;
        }

        public void EnemyKilled()
        {
            if (!isServer) return;

            _score += pointsPerEnemy;
        }

        public void WallRepaired()
        {
            if (!isServer || _score < wallRepairCost || WallHealth.IsFullHealth) return;

            _score -= wallRepairCost;
            WallHealth.ServerResetHealth();
        }

#if UNITY_EDITOR
        [ContextMenu("Cheat Score")]
        private void CheatScore()
        {
            _score = 999999999;
        }
#endif
    }
}