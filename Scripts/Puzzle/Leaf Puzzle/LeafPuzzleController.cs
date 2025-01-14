using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeafPuzzleController : BasePuzzle
{
	[SerializeField]
	private LeafController leafController;

    [SerializeField]
    private SquirrelController squirrelController;

    protected override void Start()
    {
        leafController.OnAllLeavesFallen += TurnOnSquirrelRow;
        squirrelController.OnPlayerSequenceSet += CheckPlayerSequence;

        base.Start();
    }
    private void OnDestroy()
    {
        leafController.OnAllLeavesFallen -= TurnOnSquirrelRow;
        squirrelController.OnPlayerSequenceSet -= CheckPlayerSequence;
    }

    public override void StartPuzzle()
    {
        base.StartPuzzle();

        squirrelController.SetRow(false);
        leafController.MakeLeavesFall();
    }

    private void TurnOnSquirrelRow()
    {
        squirrelController.ClearSequence();
        squirrelController.SetRow(true);
    }

    private void CheckPlayerSequence(List<LeafColourData.LeafColourKey> inputKeys)
    {
        if (inputKeys.Count == leafController.Sequence.Length)
        {
            List<LeafColourData.LeafColourKey> keysFromSequence = new();

            foreach (LeafController.LeafSpawnInfo info in leafController.Sequence)
            {
                keysFromSequence.Add(info.ColourKey);
            }

            if (inputKeys.SequenceEqual(keysFromSequence))
            {
                EndPuzzle();
            }
            else
            {
                NotifyIncorrect();
                StartPuzzle();
            }
        }
    }
}