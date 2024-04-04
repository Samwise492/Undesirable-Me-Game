using System.Collections;
using UnityEngine;

public class MusicDelayer : MonoBehaviour
{
	[SerializeField]
	private float delay;

	[SerializeField]
	private AudioSource audioSource;

	private void Start()
	{
		StartCoroutine(Delay());
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(delay);

		audioSource.Play();

		yield break;
	}
}
