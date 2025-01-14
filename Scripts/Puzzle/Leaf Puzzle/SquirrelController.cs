using LeafPuzzle;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour
{
	public event Action<List<LeafColourData.LeafColourKey>> OnPlayerSequenceSet;

	[SerializeField]
	private CanvasGroup squirrelRow;

	[SerializeField]
	private List<LeafPuzzle.Squirrel> squirrels;

	private List<LeafColourData.LeafColourKey> playerSequence = new();

    private void Start()
    {
		foreach (LeafPuzzle.Squirrel squirrel in squirrels)
		{
			squirrel.OnClicked += AddToSequence;
        }
    }
    private void OnDestroy()
    {
        foreach (LeafPuzzle.Squirrel squirrel in squirrels)
        {
            squirrel.OnClicked -= AddToSequence;
        }
    }

    public void SetRow(bool state)
	{
		squirrelRow.alpha = state ? 1 : 0;
		squirrelRow.interactable = state;
		squirrelRow.blocksRaycasts = state;
    }

    public void ClearSequence()
    {
        playerSequence.Clear();
    }

    private void AddToSequence(LeafColourData.LeafColourKey pickedColourKey)
	{
		playerSequence.Add(pickedColourKey);

		OnPlayerSequenceSet?.Invoke(playerSequence);
	}
}