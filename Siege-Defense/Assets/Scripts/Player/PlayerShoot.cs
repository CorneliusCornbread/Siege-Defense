using Mirror;
using System.Collections;
using UnityEngine;

namespace SiegeDefense.Player
{
	public class PlayerShoot : NetworkBehaviour
	{
		[SerializeField]
		private GameObject bulletPrefab;

		[SerializeField]
		[Range(0, 2)]
		private float cooldown = .2f;

		private bool _canShoot = true;

		public void OnInputShoot()
        {
			if (!isLocalPlayer) return;

			CmdSyncShoot();
        }

		[Command]
		private void CmdSyncShoot()
        {
			if (!_canShoot) return;

			StartCoroutine(ShootCooldown());
			GameObject spawn = Instantiate(bulletPrefab);
			spawn.transform.position = transform.position;
			NetworkServer.Spawn(spawn);
        }

		private IEnumerator ShootCooldown()
        {
			_canShoot = false;
			yield return new WaitForSeconds(cooldown);
			_canShoot = true;
        }
	}
}