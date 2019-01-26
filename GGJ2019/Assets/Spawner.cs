using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct waveInfos
{
    public int elicount;
    public int wormcount;
    public int lutincount;
}

public class Spawner : MonoBehaviour
{

    public class Wave
    {
        public GameObject _EliMonsterPrefab;
        public GameObject _WormMonsterPrefab;
        public GameObject _LutinMonsterPrefab;
        public float _rate;
        public waveInfos infos;
    }

    public Wave[] _waves;
    private int _nextWave = 0;

    public float _timeBeforeNext = 10f;
    public float _waveCountDown;

    void start()
    {

    }

}