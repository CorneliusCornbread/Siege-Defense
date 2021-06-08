using Mirror;
using UnityEngine;

namespace SiegeDefense.Player
{
	public class PlayerShoot : NetworkBehaviour
	{
		[SerializeField]
		private GameObject bulletPrefab;

		public void OnInputShoot()
        {
			CmdSyncShoot();
        }

		[Command]
		private void CmdSyncShoot()
        {
			GameObject spawn = Instantiate(bulletPrefab);
			spawn.transform.position = transform.position;
			NetworkServer.Spawn(spawn);
        }
	}
}