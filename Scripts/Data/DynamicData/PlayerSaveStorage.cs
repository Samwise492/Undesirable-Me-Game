using System;
using System.Collections.Generic;

public struct PlayerSaveStorage
{
    public Dictionary<int, PlayerSave> saves;

    public int lastSaveIndex;

    public struct PlayerSave
    {
        public List<int> passedDialogueNumbers;
        public List<string> passedTriggerGuids;

        public float playerPositionX;
        public float playerPositionY;
        public float playerPositionZ;
        
        public int stageDay;
        public string stage;

        public PackedSceneData lastPackedSceneData;

        public bool isGreeted;

        public PlayerProgress progress;
    }
}

[Serializable]
public struct PlayerProgress
{
    public Dictionary<int, bool> playerKeys;

    public Dictionary<StoryPointsType, int> playerPoints;
}