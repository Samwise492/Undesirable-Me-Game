using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
	[SerializeField]
	private List<SceneDataToUnpack> sceneDataToUnpack;

	private void Start()
	{
		Unpack();
    }

	private void Unpack()
	{
		foreach (SceneDataToUnpack data in sceneDataToUnpack)
		{
			if (data.Meta == LoadingManager.Instance.sceneData.lastTransferedSceneMeta)
			{
				data.SetRoots(true);
            }
			else
			{
				data.SetRoots(false);
            }
        }
    }
}

[Serializable]
public class SceneDataToUnpack
{
	public string Meta => meta;

    [SerializeField]
	private string meta;
	[SerializeField]
	private List<GameObject> rootsToOn;

	public void SetRoots(bool state)
	{
		foreach (GameObject root in rootsToOn)
		{
			root.gameObject.SetActive(state);
		}
    }
}

[Serializable]
public class PackedSceneData
{
	public string sceneToLoadName;
	public string sceneToLoadMeta;
	public bool isLoadingScreenRequired;
}