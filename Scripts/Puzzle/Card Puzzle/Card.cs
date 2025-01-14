using System;
using UnityEngine;
using UnityEngine.UI;

namespace CardPuzzle
{
	public class Card : MonoBehaviour
	{
		public event Action<Card, bool> OnSelected;

		public CardColourData.CardColourKeys Key => key;

        [SerializeField]
		private Button button;

        [SerializeField]
        private Image upperImage;
        [SerializeField]
        private Image lowImage;

        [SerializeField]
        private AudioSource clickSound;

		private CardColourData.CardColourKeys key;

        private bool isSelected;
        private bool isCompleted;

        private void Start()
        {
			button.onClick.AddListener(SetSelection);
        }
        private void OnDestroy()
        {
			button.onClick.RemoveListener(SetSelection);
        }

        public void Complete()
        {
            isCompleted = true;
            button.interactable = false;
        }

        public void SetKey(CardColourData.CardColourKeys key, Color color)
        {
            this.key = key;
            lowImage.color = color;
        }

        public void SetSelection(bool state)
        {
            if (isCompleted)
            {
                return;
            }

            isSelected = state;

            SetImage();

            OnSelected?.Invoke(this, isSelected);
        }
        private void SetSelection()
        {
            if (isCompleted)
            {
                return;
            }

            isSelected = !isSelected;

            SetImage();

            clickSound.Play();

            OnSelected?.Invoke(this, isSelected);
        }

        public void SetImage(bool isShow)
        {
            if (isShow)
            {
                upperImage.gameObject.SetActive(false);
                lowImage.gameObject.SetActive(true);

                button.targetGraphic = lowImage;
            }
            else
            {
                upperImage.gameObject.SetActive(true);
                lowImage.gameObject.SetActive(false);

                button.targetGraphic = upperImage;
            }
        }
        private void SetImage()
        {
            if (isSelected)
            {
                upperImage.gameObject.SetActive(false);
                lowImage.gameObject.SetActive(true);

                button.targetGraphic = lowImage;
            }
            else
            {
                upperImage.gameObject.SetActive(true);
                lowImage.gameObject.SetActive(false);

                button.targetGraphic = upperImage;
            }
        }
    }
}