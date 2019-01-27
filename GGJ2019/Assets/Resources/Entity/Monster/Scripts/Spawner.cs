using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Wave
{
    public int timeBeforeWave;
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

    private float _waveEndTime;
    private float _timeBeforeNext = 10.0f;
    private float _waveStartTime = 0;
    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private float _rateDelai = 1.0f;
    private float _rateStartTime = 0;
    private bool _waveRunning = false;

    private void Start()
    {
        _waveEndTime = Time.time;
        _waveRunning = false;
        if (_waves.Count > 0)
            _timeBeforeNext = _waves[0].timeBeforeWave;
    }

    private bool SpawnGroupsOfMobs(GameObject prefab, ref int nbr, float rate)
    {
        if (prefab == null)
            return true;
        for (int i = 0; i < nbr; i++)
        {
            float tmpX, tmpY;
            if (Random.Range(0, 2) == 0)
            {
                tmpX = Random.Range(0, 2) == 0 ? World.Instance.Boundary.min.x - 1 : World.Instance.Boundary.max.x + 1;
                tmpY = Random.Range(World.Instance.Boundary.min.y - 1, World.Instance.Boundary.max.y + 2);
            }
            else
            {
                tmpY = Random.Range(0, 2) == 0 ? World.Instance.Boundary.min.y - 1 : World.Instance.Boundary.max.y + 1;
                tmpX = Random.Range(World.Instance.Boundary.min.x - 1, World.Instance.Boundary.max.x + 2);
            }
            Debug.Log("["+name+"] - Spawning at X: " + tmpX + ", tmpY: " + tmpY);
            GameObject obj = Instantiate(prefab, new Vector2(tmpX, tmpY), prefab.transform.rotation);
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
        _waveStartTime = Time.time;
        _waveRunning = true;
        waveText.enabled = true;
        waveText.text = "Wave " + (_currentWaveIndex + 1);
    }

    private void EndWave()
    {
        Debug.Log("[" + name + "] - Preparing next wave");
        _currentWaveIndex++;
        _waveRunning = false;
        _waveEndTime = Time.time;
        waveText.enabled = false;
    }

    private void RunWave()
    {
        Wave current = _waves[_nextWaveIndex];
        if (_rateStartTime + current.rate > Time.time)
            return;
        if (_waveStartTime + 3 < Time.time)
            waveText.enabled = false;
      
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
                EndWave();
                return;
            }
            else
                _waves[_currentWaveIndex] = current;
        }
    }

    private void Update()
    {
        if (_currentWaveIndex >= _waves.Count)
            return;
        if (!_waveRunning)
        {
            if (_waveEndTime + _timeBeforeNext < Time.time)
            {
                StartNextWave();
            }
            return;
        }
        RunWave();
    }
}