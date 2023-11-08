using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public List<Enemy> enemies;
    public List<WaveSpawner> waves;
    public UnityEvent onChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated GameManager, ignoring this one", gameObject);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        onChanged.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        onChanged.Invoke();
    }

    public void AddWave(WaveSpawner wave)
    {
        waves.Add(wave);
        onChanged.Invoke();
    }

    public void RemoveWave(WaveSpawner wave)
    {
        waves.Remove(wave);
        onChanged.Invoke();
    }

}
