using UnityEngine;

namespace SiegeDefense.Player
{
	public class PlayerCounter : MonoBehaviour
	{
        public static int Count => _count;
        private static int _count = 0;

        private void OnEnable()
        {
            _count++;
        }

        private void OnDisable()
        {
            _count--;
        }
    }
}