using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class PointsSpawnSystem : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private ATMPool atmPool;
    [SerializeField]
    private int spawnLimit;

    private List<Transform> spawnPoints = new();
    private int spawnedATMs = 0;


    public int SpawnedATMs
    {
        get => spawnedATMs;
        set => spawnedATMs = value;
    }


    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        foreach(var point in GameObject.FindGameObjectsWithTag("SpawnPoint"))
            spawnPoints.Add(point.transform);
        StartCoroutine(SpawnRoutine());
    }

    #endregion

    #region MAIN_METHODS

    private IEnumerator SpawnRoutine()
    {
        int amountToSpawn = spawnLimit;
        while (true)
        {
            if (spawnedATMs < spawnLimit)
            {
                print("PayDay");
                for (int i = 0; i < amountToSpawn; i++)
                {
                    Transform _randomPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    spawnPoints.Remove(_randomPosition);
                    atmPool.SpawnATM(_randomPosition.position, _randomPosition.rotation);
                    SpawnedATMs++;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            amountToSpawn = spawnLimit - spawnedATMs;
        }       
    }

    public void ReleasePoint(Transform spawnPoint)
    {
        spawnPoints.Add(spawnPoint);
        SpawnedATMs--;
    }

    #endregion

}
