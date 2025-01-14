using UnityEngine;
using UnityEngine.UI;

namespace LeafPuzzle
{
    public class Leaf : MonoBehaviour
    {
        public float speed;

        [SerializeField]
        private Image image;

        public void SetColour(Color color)
        {
            image.color = color;
        }

        private void Update()
        {
            transform.localPosition -= new Vector3(0, speed, 0);
        }
    }
}