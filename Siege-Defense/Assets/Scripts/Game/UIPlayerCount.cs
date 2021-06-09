using TMPro;
using UnityEngine;

namespace SiegeDefense.Game
{
	public class UIPlayerCount : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text text;

		[SerializeField]
		private string label = "Players:";

        private void OnGUI()
        {
			text.text = label + " " + Player.PlayerCounter.Count;
        }
    }
}