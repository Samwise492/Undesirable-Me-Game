using LeafPuzzle;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LeafController : MonoBehaviour
{
    public event Action OnAllLeavesFallen;

    public LeafSpawnInfo[] Sequence => sequence;

    [SerializeField]
    private LeafColourData colourData;

    [SerializeField]
    private Leaf leafPrefab;

    [SerializeField]
    private LeafSpawnInfo[] sequence;

    [SerializeField]
    private float startDelay;

    [SerializeField]
    private float spawnDelay;

    [SerializeField]
    private float leafDestroyTime;

    private int spawnedLeafCount;
    private int destroyedLeafCount;

    public void MakeLeavesFall()
    {
        spawnedLeafCount = 0;
        destroyedLeafCount = 0;

        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(startDelay);

        StartCoroutine(SpawnLeaves());

        yield break;
    }

    private IEnumerator SpawnLeaves()
    {
        foreach (LeafSpawnInfo info in sequence)
        {
            yield return new WaitForSeconds(spawnDelay);

            Leaf spawnedLeaf = Instantiate(leafPrefab, info.SpawnPoint);
            spawnedLeaf.SetColour(colourData.ColourData.Where(x => x.ColourKey == sequence[spawnedLeafCount].ColourKey)
                .First().Color);
            spawnedLeafCount++;

            StartCoroutine(PlanLeafDestroy(spawnedLeaf));
        }

        yield break;
    }

    private IEnumerator PlanLeafDestroy(Leaf leaf)
    {
        yield return new WaitForSeconds(leafDestroyTime);

        Destroy(leaf.gameObject);
        destroyedLeafCount++;

        if (destroyedLeafCount == sequence.Length)
        {
            OnAllLeavesFallen?.Invoke();
        }

        yield break;
    }

    [Serializable]
    public class LeafSpawnInfo
    {
        public Transform SpawnPoint => spawnPoint;
        public LeafColourData.LeafColourKey ColourKey => colourKey;

        [SerializeField]
        private LeafColourData.LeafColourKey colourKey;

        [SerializeField]
        private Transform spawnPoint;
    }
}
