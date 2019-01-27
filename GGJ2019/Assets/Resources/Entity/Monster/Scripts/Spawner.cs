using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Wave
{
    public int eliNumber;
    public int wormNumber;
    public int lutinNumber;

    public float rate;
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
    private TextMeshProUGUI waveText;

    [SerializeField]
    private List<Wave> _waves = new List<Wave>();
    private int _nextWaveIndex = 0;

    private float _timeBeforeNext = 10.0f;
    private float _waveStartTime = 0;

    private float _rateDelai = 1.0f;
    private float _rateStartTime = 0;
    private bool _waveRunning = false;

    private void Start()
    {
        _waveStartTime = Time.time + _timeBeforeNext / 2f;
    }

    private bool SpawnGroupsOfMobs(GameObject prefab, ref int nbr, float rate)
    {
        if (prefab == null)
            return true;
        for (int i = 0; i < nbr; i++)
        {
            int tmpX, tmpY;
            if (Random.Range(0, 2) == 0)
            {
                tmpX = Random.Range(0, 2) == 0 ? (int)World.Instance.Boundary.min.x - 1 : (int)World.Instance.Boundary.max.x + 1;
                tmpY = Random.Range((int)World.Instance.Boundary.min.y - 1, (int)World.Instance.Boundary.max.y + 2);
            }
            else
            {
                tmpY = Random.Range(0, 2) == 0 ? (int)World.Instance.Boundary.min.y - 1 : (int)World.Instance.Boundary.max.y + 1;
                tmpX = Random.Range((int)World.Instance.Boundary.min.x - 1, (int)World.Instance.Boundary.max.x + 2);
            }
            
            GameObject obj = Instantiate(prefab, new Vector3(tmpX, tmpY, 0), prefab.transform.rotation);
            Debug.Log("[" + name + "] - Spawning mob: "+ obj.name);
            nbr--;
            return false;
        }
        nbr = 0;
        return true;
    }

    private int GetRandomMobToSpawnId(Wave current)
    {
        List<int> availables = new List<int>();
        if (current.eliNumber > 0)
            availables.Add(0);
        if (current.wormNumber > 0)
            availables.Add(1);
        if (current.lutinNumber > 0)
            availables.Add(2);
        return availables.Count > 0 ? availables[Random.Range(0, availables.Count)] : -1;
    }

    private void StartNextWave()
    {
        if (_nextWaveIndex >= _waves.Count)
            return;
        Wave current = _waves[_nextWaveIndex];
        if (_rateStartTime + current.rate > Time.time)
            return;
        if (_waveStartTime + _timeBeforeNext - Time.time > 5)
            waveText.enabled = false;
        if (!_waveRunning)
        {
            _waveRunning = true;
            waveText.enabled = true;
            waveText.text = "Wave " + (_nextWaveIndex + 1);
        }
        Debug.Log("[" + name + "] - Starting spawning mobs...");
        _rateStartTime = Time.time;
        int id = GetRandomMobToSpawnId(current);
        if (id != -1)
        {
            bool result = true;
            switch (id)
            {
                case 0:
                    result = SpawnGroupsOfMobs(eliMonsterPrefab, ref current.eliNumber, current.rate);
                    break;
                case 1:
                    result = SpawnGroupsOfMobs(wormMonsterPrefab, ref current.wormNumber, current.rate);
                    break;
                case 2:
                    result = SpawnGroupsOfMobs(lutinMonsterPrefab, ref current.lutinNumber, current.rate);
                    break;
            }
            if (!result)
            {
                _waves.RemoveAt(_nextWaveIndex);
                _waves.Insert(_nextWaveIndex, current);
                return;
            }
        }
        Debug.Log("[" + name + "] - Preparing next wave");
        _nextWaveIndex++;
        _waveStartTime = Time.time;
        _waveRunning = false;
        waveText.enabled = false;
    }

    private void Update()
    {
        if (_waveStartTime + _timeBeforeNext < Time.time)
            StartNextWave();
    }
}