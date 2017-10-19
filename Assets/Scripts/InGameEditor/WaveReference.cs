using System;
using UnityEngine;
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
[Serializable]
public class WaveReference : MonoBehaviour {
    public string nameOfWave = "";
    public WaveDifficulty difficulty = WaveDifficulty.easy;
    public int numberOfEnemies = 0;
    public int numberOfItems = 0;    
}
