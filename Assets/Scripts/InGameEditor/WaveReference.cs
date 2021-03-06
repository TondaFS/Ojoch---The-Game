﻿using System;
using UnityEngine;
using System.Collections.Generic;
using SmartLocalization;

/// <summary>
/// Jednotlive obtiznosti nepratelskych vln
/// </summary>
[Serializable]
public enum WaveDifficulty
{
    easy,
    medium,
    hard
}

/// <summary>
/// Trida, reprezentujici vytvorenou vlnu, obsahuje jmeno, obtiynost a pocet nepratel a powerupu, ktere v sobe ma
/// </summary>
public class WaveReference : MonoBehaviour{
    /// <summary>
    /// Name of the wave
    /// </summary>
    public string nameOfWave = "";
    /// <summary>
    /// Difficulty of the wave
    /// </summary>
    public WaveDifficulty difficulty = WaveDifficulty.easy;
    /// <summary>
    /// List of all enemies
    /// </summary>
    public List<EditorObject> Enemies;
    /// <summary>
    /// List of all powerUps
    /// </summary>
    public List<EditorObject> PowerUps;
    /// <summary>
    /// Byla vlna ulozena od posledni zmeny?
    /// </summary>
    public bool isSaved = false;  

    void Start()
    {
        if (Enemies == null)
        {
            Enemies = new List<EditorObject>();
            PowerUps = new List<EditorObject>();
            nameOfWave = LanguageManager.Instance.GetTextValue("Editor.NewWaveName");
        }
    }
}
