using UnityEngine;
using UnityEngine.UI;
using WeightPuzzle;

[RequireComponent(typeof(Highlighter))]
[RequireComponent(typeof(Weight))]
public class WeightToHighlighterAdapter : MonoBehaviour
{
	private Weight weight => GetComponent<Weight>();
	private Highlighter highlighter => GetComponent<Highlighter>();

    private Image image => weight.GetComponent<Image>();

    private void OnEnable()
    {
        weight.OnSelected += SetHighlight;
    }
    private void OnDisable()
    {
        weight.OnSelected -= SetHighlight;
    }

    private void SetHighlight(Weight weight, bool isSelected)
    {
        highlighter.SetHighlight(image, isSelected);
    }
}
