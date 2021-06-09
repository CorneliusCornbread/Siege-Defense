using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace SiegeDefense.Game
{
	[RequireComponent(typeof(TMP_Text))]
	public class UIScore : MonoBehaviour
	{
		[SerializeField]
		[Required]
		private TMP_Text text;

		[SerializeField]
		private string scoreLabel = "Score:";

        private void OnGUI()
        {
			text.text = scoreLabel + " " + ScoreManager.Instance.Score;
        }
    }
}