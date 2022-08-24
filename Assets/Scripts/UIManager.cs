using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshPro gameWinText;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
