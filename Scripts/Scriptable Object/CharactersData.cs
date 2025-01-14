using System;
using UnityEngine;

public class CharactersData : ScriptableObject
{
    [SerializeField]
    private CharacterEmotions[] emotionsData;

    public Sprite GetEmotionSprite(Characters character, Moods mood)
    {
        foreach (CharacterEmotions data in emotionsData)
        {
            if (data.Character == character)
            {
                foreach (CharacterEmotions.EmotionInfo _info in data.Info)
                {
                    if (_info.Mood == mood)
                    {
                        return _info.Sprite;
                    }
                }
            }
        }

        return Sprite.Create(null, new Rect(0, 0, 0, 0), Vector2.zero);
    }

    [Serializable]
    private class CharacterEmotions
    {
        public Characters Character => character;
        public EmotionInfo[] Info => info;
        
        [SerializeField] 
        private Characters character;
        [SerializeField]
        private EmotionInfo[] info;

        [Serializable]
        public class EmotionInfo
        {
            public Sprite Sprite => sprite;
            public Moods Mood => mood;

            [SerializeField]
            private Moods mood;
            [SerializeField]
            private Sprite sprite;

        }
    }
}

public enum Characters
{
    Jack,
    McKinsey,
    DoctorShepard,
    Aubrey,
    Leah,
    Jason,
    ManOnStreet,
    ChildDoctor
}
public enum Moods
{
    Surprise,
    Sadness,
    Calmness,
    Anger,
    Happiness,
    Fear,
    Thinking
}