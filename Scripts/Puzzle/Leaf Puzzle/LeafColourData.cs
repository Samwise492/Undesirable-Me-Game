using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/Create Leaf Colour Data")]
public class LeafColourData : ScriptableObject
{
    public List<LeafColourContainer> ColourData => colourData;

    [SerializeField]
	private List<LeafColourContainer> colourData;

	[Serializable]
	public class LeafColourContainer
	{
		public LeafColourKey ColourKey => colourKey;
		public Color Color => color;

        [SerializeField]
		private LeafColourKey colourKey;
		[SerializeField]
        private Color color;
	}

    public enum LeafColourKey
    {
        White,
        Salad,
        Pink,
        Purple,
        Green
    }
}
