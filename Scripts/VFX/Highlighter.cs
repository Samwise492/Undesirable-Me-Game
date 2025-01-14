using UnityEngine;
using UnityEngine.UI;

public class Highlighter : MonoBehaviour
{
	[HideInInspector]
	public Image imageComponent;

    [SerializeField]
    private float step;

    [SerializeField]
    private float downLimit;

    private bool isHighlight;
	private bool isIncrease;

	public void SetHighlight(Image image, bool state)
	{
		imageComponent = image;

        isHighlight = state;

		if (!state)
		{
			imageComponent.color = Color.white;
        }
    }

    private void Update()
    {
        if (isHighlight)
		{
			if (!isIncrease)
			{
				imageComponent.color -= new Color(0, 0, 0, step);

				if (imageComponent.color.a <= downLimit)
				{
					isIncrease = true;
				}
			}
			else
			{
				imageComponent.color += new Color(0, 0, 0, step);

                if (imageComponent.color.a >= 1)
                {
					isIncrease = false;
                }
            }
        }
    }
}
