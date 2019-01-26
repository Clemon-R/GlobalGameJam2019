using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public int eliNumber;
    public int wormNumber;
    public int lutinNumber;

    public int rate;
}
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject eliMonsterPrefab;
    [SerializeField]
    private GameObject wormMonsterPrefab;
    [SerializeField]
    private GameObject lutinMonsterPrefab;

    [SerializeField]
    private List<Wave> _waves = new List<Wave>();
    private int _nextWaveIndex = 0;

    private float _timeBeforeNext = 10.0f;
    private float _waveStartTime = 0;

    private float _rateDelai = 1.0f;
    private float _rateStartTime = 0;

    private void Start()
    {
        _waveStartTime = Time.time;
        //StartNextWave();
    }

    private bool SpawnGroupsOfMobs(GameObject prefab, ref int nbr, int rate)
    {
        if (prefab == null)
            return true;
        int tmp = 0;

        for (int i = 0; i < nbr; i++)
        {
            tmp++;
            int tmpX, tmpY;
            if (Random.Range(0, 2) == 0)
            {
                tmpX = Random.Range(0, 2) == 0 ? (int)World.Instance.Boundary.min.x : (int)World.Instance.Boundary.max.x;
                tmpY = Random.Range((int)World.Instance.Boundary.min.y, (int)World.Instance.Boundary.max.y);
            }
            else
            {
                tmpY = Random.Range(0, 2) == 0 ? (int)World.Instance.Boundary.min.y : (int)World.Instance.Boundary.max.y;
                tmpX = Random.Range((int)World.Instance.Boundary.min.x, (int)World.Instance.Boundary.max.x);
            }
            
            GameObject obj = Instantiate(prefab, new Vector3(tmpX, tmpY, 0), prefab.transform.rotation);
            Debug.Log("[" + name + "] - Spawning mob: "+ obj.name);
            if (tmp >= rate)
            {
                nbr -= tmp;
                return false;
            }
        }
        nbr = 0;
        return true;
    }

    private void StartNextWave()
    {
        if (_nextWaveIndex >= _waves.Count)
            return;
        if (_rateStartTime + _rateDelai > Time.time)
            return;
        Debug.Log("[" + name + "] - Starting spawning mobs...");
        _rateStartTime = Time.time;
        Wave current = _waves[_nextWaveIndex];
        Debug.Log("[" + name + "] - Eli nbr: " + current.eliNumber);
        if (!SpawnGroupsOfMobs(eliMonsterPrefab, ref current.eliNumber, current.rate) ||
            !SpawnGroupsOfMobs(wormMonsterPrefab, ref current.wormNumber, current.rate) ||
            !SpawnGroupsOfMobs(lutinMonsterPrefab, ref current.lutinNumber, current.rate))
        {
            _waves.RemoveAt(_nextWaveIndex);
            _waves.Insert(0, current);
            return;
        }
        Debug.Log("[" + name + "] - Preparing next wave");
        _nextWaveIndex++;
        _waveStartTime = Time.time;
    }

    private void Update()
    {
        if (_waveStartTime + _timeBeforeNext > Time.time)
            StartNextWave();
    }
}