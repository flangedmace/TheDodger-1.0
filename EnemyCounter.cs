using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;

    public int Count { get; private set; }

    private void Start()
    {
        Count = 0;
    }

    public void PlusCount()
    {
        Count++;
        _countText.text = Convert.ToString(Count);
    }
}
