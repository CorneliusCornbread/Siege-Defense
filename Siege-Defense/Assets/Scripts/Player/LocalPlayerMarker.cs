using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiegeDefense.Player
{
	public class LocalPlayerMarker : NetworkBehaviour
	{
		[SerializeField]
		[Required]
		private SpriteRenderer rend;

        [SerializeField]
        private Color localColor;

        private void Start()
        {
            if (isLocalPlayer)
            {
                rend.color = localColor;
            }
        }
    }
}