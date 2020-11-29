using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _distanceToBorder;
    [SerializeField] private UnityEvent _finishLevel;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;

    private int _enemiesOnThisWave;
    private int _allenemiesOnThisWave;

    private bool _spawning;

    private void Start()
    {
        SetWave(_currentWaveNumber);
        _spawning = true;
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        if (_spawning)
        {
            for (int i = 0; i < _currentWave.SpawnedObjectTypesCount; i++)
            {
                _currentWave.GetCertainSpawnedObject(i).TimeAfterLastSpawn += Time.deltaTime;

                if (_currentWave.GetCertainSpawnedObject(i).TimeAfterLastSpawn >= _currentWave.GetCertainSpawnedObject(i).SpawnDelay)
                {
                    if(_currentWave.GetCertainSpawnedObject(i).Spawned < _currentWave.GetCertainSpawnedObject(i).Count)
                    {
                        InstantiateEnemy(i);

                        _enemiesOnThisWave++;
                        _currentWave.GetCertainSpawnedObject(i).PlusSpawned();

                        _currentWave.GetCertainSpawnedObject(i).TimeAfterLastSpawn = 0f;
                    }
                }
            }
        }
        
        if (_enemiesOnThisWave >= _allenemiesOnThisWave)
        {
            if (_waves.Count - 1 > _currentWaveNumber)
                NextWave();
            else
                _spawning = false;
        }

        if (_enemyCounter.Count >= GetAllEnemiesOnLevel())
            _finishLevel.Invoke();
    }

    
    private void InstantiateEnemy(int enemyIndex)
    {
        Vector2 spawPoint = new Vector2(_spawnPoint.position.x + Random.Range(-_distanceToBorder, _distanceToBorder), _spawnPoint.position.y);

        Instantiate(_currentWave.GetCertainSpawnedObject(enemyIndex).Template, spawPoint, _spawnPoint.rotation).GetComponent<Enemy>();
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];

        _allenemiesOnThisWave = GetAllEnemiesOnThisWave();
        _enemiesOnThisWave = 0;
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);
    }

    private int GetAllEnemiesOnThisWave()
    {
        int allenemiesOnThisWave = 0;

        for (int i = 0; i < _currentWave.SpawnedObjectTypesCount; i++)
        {
            allenemiesOnThisWave += _currentWave.GetCertainSpawnedObject(i).Count;
        }

        return allenemiesOnThisWave;
    }


    private int GetAllEnemiesOnLevel()
    {
        int allEnemiesOnLevel = 0;

        for (int i = 0; i < _waves.Count; i++)
        {
            for (int j = 0; j < _waves[i].SpawnedObjectTypesCount; j++)
            {
                allEnemiesOnLevel += _waves[i].GetCertainSpawnedObject(j).Count;
            }
        }

        return allEnemiesOnLevel;
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private List<SpawnedObject> _spawnedObjects;

    public int SpawnedObjectTypesCount => _spawnedObjects.Count;

    public SpawnedObject GetCertainSpawnedObject(int enemyIndex)
    {
        return _spawnedObjects[enemyIndex];
    }
}

[System.Serializable]
public class SpawnedObject
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _count;

    [HideInInspector] public float TimeAfterLastSpawn;

    private int _spawned;

    public int Spawned => _spawned;
    public int Count => _count;
    public float SpawnDelay => _spawnDelay;
    public GameObject Template => _template;

    public SpawnedObject()
    {
        TimeAfterLastSpawn = 0f;
        _spawned = 0;
    }

    public void PlusSpawned()
    {
        _spawned++;
    }
}