using UnityEngine;

public static class SoundPitcher
{
	public static void PlayRandomPitch(AudioSource source, float pitchMin, float pitchMax)
	{
		float startPitch = source.pitch;

        source.pitch = Random.Range(pitchMin, pitchMax);

		source.Play();

		//source.pitch = startPitch;
	}
}
