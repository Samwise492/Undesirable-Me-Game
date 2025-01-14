using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/Create Card Colour Database")]
public class CardColourData : ScriptableObject
{
    public CardColourElement[] Data => data;

    [SerializeField]
    private CardColourElement[] data;

    [Serializable]
    public class CardColourElement
    {
        public CardColourKeys ColourKey => colourKey;
        public Color Colour => colour;

        [SerializeField]
        private CardColourKeys colourKey;

        [SerializeField]
        private Color colour;
    }

    public enum CardColourKeys
    {
        Red,
        Yellow,
        Purple,
        Pink,
        Green,
        Orange,
        Turquoise,
        White,
        Blue
    }
}
