using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life playerLife;
    [SerializeField] Life baseLife;

    private void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerOrBaseDied);
        baseLife.onDeath.AddListener(OnPlayerOrBaseDied);
        GameManager.instance.onChanged.AddListener(CheckWinCondition);
    }
    void CheckWinCondition()
    {
        if(GameManager.instance.enemies.Count <= 0 && GameManager.instance.waves.Count <= 0 && baseLife.amount > 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    void OnPlayerOrBaseDied()
    {
        SceneManager.LoadScene("LoseScreen");
    }
}
