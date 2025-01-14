using System;
using UnityEngine;
using UnityEngine.UI;

namespace LeafPuzzle
{
	public class Squirrel : MonoBehaviour
	{
        public event Action<LeafColourData.LeafColourKey> OnClicked;

		[SerializeField]
		private LeafColourData.LeafColourKey colour;

        [SerializeField]
        private Button button;

        [SerializeField]
        private AudioSource clickSound;

        private void Start()
        {
            button.onClick.AddListener(NotifyClick);
        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(NotifyClick);
        }

        private void NotifyClick()
        {
            SoundPitcher.PlayRandomPitch(clickSound, 0.5f, 1.2f);

            OnClicked?.Invoke(colour);
        }
    }
}