using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeightPuzzle
{
    public class WeightPuzzleController : BasePuzzle
    {
        [SerializeField]
        private List<Weight> firstWeightRow;
        [SerializeField]
        private List<Weight> secondWeightRow;

        private readonly List<Weight> weights = new();

        private Weight currentSelectedWeight;

        private bool wasSelected;

        private void OnDestroy()
        {
            foreach (Weight weight in weights)
            {
                weight.OnSelected -= CheckWeightSelection;
                weight.OnSelected -= ReplaceWeights;
            }
        }

        public override void StartPuzzle()
        {
            base.StartPuzzle();

            AssignWeights();

            foreach (Weight weight in weights)
            {
                weight.OnSelected += CheckWeightSelection;
                weight.OnSelected += ReplaceWeights;
            }
        }

        private void CheckWeightSelection(Weight weight, bool isSelected)
        {
            if (wasSelected)
            {
                return;
            }

            if (currentSelectedWeight == null)
            {
                currentSelectedWeight = weight;
            }
            else
            {
                if (currentSelectedWeight == weight)
                {
                    wasSelected = true;
                    UnselectAll();
                }
            }
        }

        private void ReplaceWeights(Weight weight, bool isSelected)
        {
            if (isSelected && currentSelectedWeight)
            {
                if (weight != currentSelectedWeight)
                {
                    float previousWeight = currentSelectedWeight.WeightValue;
                    float newWeight = weight.WeightValue;

                    currentSelectedWeight.ChangeWeight(newWeight);
                    weight.ChangeWeight(previousWeight);

                    UnselectAll();

                    if (GetTotalWeight(firstWeightRow) == GetTotalWeight(secondWeightRow))
                    {
                        EndPuzzle();
                    }
                }
            }
        }

        private void UnselectAll()
        {
            foreach (Weight weight in weights)
            {
                weight.SetSelection(false);
            }

            currentSelectedWeight = null;
            wasSelected = false;
        }

        private void AssignWeights()
        {
            foreach (Weight weight in firstWeightRow)
            {
                weights.Add(weight);
            }
            foreach (Weight weight in secondWeightRow)
            {
                weights.Add(weight);
            }
        }

        private float GetTotalWeight(List<Weight> weightRow)
        {
            float totalWeight = 0;

            foreach (Weight weight in weightRow)
            {
                totalWeight += weight.WeightValue;
            }

            return totalWeight;
        }
    }
}