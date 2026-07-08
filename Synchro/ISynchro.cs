using System;
using UnityEngine;
public delegate void BeatFunc(int beatCount, double timeCode);
public abstract class ISynchro : MonoBehaviour
{
    public static ISynchro Instance { get; protected set; }
    public BeatFunc PlayOnBeat;// to schedule sounds
    public TempoData Tempo;
    [Header("Read Only")]
    public int CurrentBeatCount;
    public double CurrentBeat, CurrentBar, NextBeat;//current bar is past
    public abstract void StartSynchro(double startTime);
    public abstract void StopSynchro();
    public double GetNextBar(int barsAhead = 1)
    {
        return CurrentBar + Tempo.BarLength * barsAhead;
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
[Serializable]
public class TempoData
{
    public enum BarValue
    {
        Quarter = 1,
        Eight,
        Triplet,
        Sixteen,
    }
    public int CrotchetBPM = 120;
    public int BeatsPerBar = 4;
    public BarValue TimeSignature = BarValue.Quarter;
    public int BPM => CrotchetBPM * (int)TimeSignature;
    public double BeatLength => (double)60 / BPM;
    public double BarLength => BeatLength * BeatsPerBar;
}