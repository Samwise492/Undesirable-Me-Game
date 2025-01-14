using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WeightPuzzle
{
    public class Weight : MonoBehaviour
    {
        public event Action<Weight, bool> OnSelected;

        public float WeightValue => weight;

        [SerializeField]
        private float weight;

        [Header("UI")]
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI text;

        private bool isSelected;

        private void Start()
        {
            button.onClick.AddListener(SetSelection);

            SetRepresentation();
        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(SetSelection);
        }

        public void ChangeWeight(float weight)
        {
            this.weight = weight;

            SetRepresentation();
        }

        public void SetSelection()
        {
            isSelected = !isSelected;

            OnSelected?.Invoke(this, isSelected);
        }
        public void SetSelection(bool state)
        {
            isSelected = state;

            OnSelected?.Invoke(this, isSelected);
        }

        private void SetRepresentation()
        {
            text.text = weight.ToString();
        }
    }
}