using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemiesUI : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        GameManager.instance.onChanged.AddListener(RefreshText);
    }

    void RefreshText()
    {
        text.text = "Remaining Enemies: " + GameManager.instance.enemies.Count;
    }
}
