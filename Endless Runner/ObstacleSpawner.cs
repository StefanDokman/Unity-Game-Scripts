using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Lane Settings")]
    public float[] laneOffsetsX = new float[] { -2f, 0f, 2f };

    [Header("Obstacle Prefabs")]
    public GameObject[] jumpableObstacles;
    public GameObject[] rollableObstacles;
    public GameObject[] unpassableObstacles;
    public GameObject coinPrefab;

    [Header("Segment Settings")]
    public int lookaheadSegments = 3;
    [Range(0f, 1f)] public float emptySegmentChance = 0.3f;
    public int coinGroupMin = 4;
    public float spawnDelay = 1.0f;

    private List<bool[]> upcomingSegments = new List<bool[]>();
    private int spawnedSegmentsCount = 0;

    private Coroutine spawnCoroutine;

    void Start()
    {
        for (int i = 0; i < lookaheadSegments; i++)
        {
            upcomingSegments.Add(new bool[3] { false, false, false });
        }
        spawnCoroutine = StartCoroutine(SpawnSegmentsCoroutine());
    }

    void Update()
    {
        MoveChildrenBackward();
    }

    IEnumerator SpawnSegmentsCoroutine()
    {
        while (true)
        {
            SpawnNextSegment();
            spawnedSegmentsCount++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnNextSegment()
    {
        bool[] segmentUnpassable = new bool[3];

        if (Random.value >= emptySegmentChance)
        {
            List<int> candidateLanes = new List<int> { 0, 1, 2 };

            foreach (var seg in upcomingSegments)
            {
                for (int lane = 0; lane < 3; lane++)
                {
                    if (seg[lane])
                        candidateLanes.Remove(lane);
                }
            }

            int maxBlock = Mathf.Min(2, candidateLanes.Count);
            int lanesToBlock = Random.Range(0, maxBlock + 1);

            for (int i = 0; i < lanesToBlock; i++)
            {
                int idx = Random.Range(0, candidateLanes.Count);
                int lane = candidateLanes[idx];
                segmentUnpassable[lane] = true;
                candidateLanes.RemoveAt(idx);
            }
        }

        upcomingSegments.Add(segmentUnpassable);
        if (upcomingSegments.Count > lookaheadSegments)
            upcomingSegments.RemoveAt(0);

        for (int lane = 0; lane < 3; lane++)
        {
            Vector3 worldPos = transform.position + new Vector3(laneOffsetsX[lane], 0f, 0f);

            if (segmentUnpassable[lane])
            {
                SpawnRandomObstacle(unpassableObstacles, worldPos, true);
            }
            else
            {
                float r = Random.value;
                if (r < 0.4f)
                {
                    SpawnRandomObstacle(jumpableObstacles, worldPos, false);
                    if (Random.value < 0.5f)
                        SpawnCoinGroup(worldPos + new Vector3(0, 1f, 1f));
                }
                else if (r < 0.7f)
                {
                    SpawnRandomObstacle(rollableObstacles, worldPos, false);
                }
                else
                {
                    if (Random.value < 0.3f)
                        SpawnCoinGroup(worldPos);
                }
            }
        }
    }

    void SpawnRandomObstacle(GameObject[] pool, Vector3 worldPos, bool isUnpassable)
    {
        if (pool.Length == 0) return;

        int index = Random.Range(0, pool.Length);
        GameObject obj = Instantiate(pool[index]);
        obj.transform.position = worldPos;
        if(!isUnpassable)
        obj.transform.rotation = Quaternion.identity;
        else
        obj.transform.rotation = Quaternion.Euler(0,180,0);
        obj.transform.SetParent(transform);
    }

    void SpawnCoinGroup(Vector3 worldStartPos)
    {
        for (int i = 0; i < coinGroupMin; i++)
        {
            Vector3 worldPos = worldStartPos + new Vector3(0, 2, i*4);
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.position = worldPos; 
            coin.transform.rotation = Quaternion.identity;
            coin.transform.SetParent(transform);
        }
    }

    void MoveChildrenBackward()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }
}
