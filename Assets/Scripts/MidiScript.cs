using UnityEngine;
using NAudio.Midi;
using NAudio;
using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

public class MidiScript : MonoBehaviour
{
    public List<int> track1 = new List<int>();
    public List<int> track2 = new List<int>();
    public List<int> track3 = new List<int>();
    public List<int> track4 = new List<int>();
    public List<int> track5 = new List<int>();
    public string fileName = "Assets/MIDI/rush_e_real.mid";
    public string midiString;
    [SerializeField]
    private NoteScript finalObject;
    private string ToMBT(long eventTime, int ticksPerQuarterNote, TimeSignatureEvent timeSignature)
    {
        int beatsPerBar = timeSignature == null ? 4 : timeSignature.Numerator;
        int ticksPerBar = timeSignature == null ? ticksPerQuarterNote * 4 : (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
        int ticksPerBeat = ticksPerBar / beatsPerBar;
        long bar = 1 + (eventTime / ticksPerBar);
        long beat = 1 + ((eventTime % ticksPerBar) / ticksPerBeat);
        long tick = eventTime % ticksPerBeat;
        return String.Format("{0}:{1}:{2}", bar, beat, eventTime);
    }
    public void LoadMidiFile()
    {
        var strictMode = false;
        var mf = new MidiFile(fileName, strictMode);
        Debug.Log("Format {0}, Tracks {1}, Delta Ticks Per Quarter Note {2}" +
                mf.FileFormat + " " + mf.Tracks + " " + mf.DeltaTicksPerQuarterNote);
        var timeSignature = mf.Events[0].OfType<TimeSignatureEvent>().FirstOrDefault();
        for (int n = 0; n < mf.Tracks; n++)
        {
            foreach (var midiEvent in mf.Events[n])
            {
                if (!MidiEvent.IsNoteOff(midiEvent))
                {
                    midiString = midiEvent.ToString();
                    track1.Add(-1);
                    track2.Add(-1);
                    track3.Add(-1);
                    track4.Add(-1);
                    track5.Add(-1);
                    if (midiString.Contains("A6") || midiString.Contains("B6") || midiString.Contains("C6") || midiString.Contains("D6") || midiString.Contains("E6") || midiString.Contains("F6") || midiString.Contains("G6"))
                    {
                        NoteScript newNote = Instantiate(finalObject, new Vector3(700, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                        track1.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        
                    }
                    else if (midiString.Contains("A5") || midiString.Contains("B5") || midiString.Contains("C5") || midiString.Contains("D5"))
                    {
                        if (track2[track2.Count - 1] >= (midiEvent.AbsoluteTime - 50))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(830, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track2.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                    else if (midiString.Contains("E5") || midiString.Contains("F5") || midiString.Contains("G5") || midiString.Contains("A4"))
                    {
                        if (track3[track3.Count - 1] >= (midiEvent.AbsoluteTime - 50))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(960, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track3.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                    else if (midiString.Contains("B4") || midiString.Contains("C4") || midiString.Contains("D4") || midiString.Contains("E4"))
                    {
                        if (track4[track4.Count - 1] >= (midiEvent.AbsoluteTime - 50))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(1090, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track4.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                    else if (midiString.Contains("F4") || midiString.Contains("G4") || midiString.Contains("E3") || midiString.Contains("F3") || midiString.Contains("G3") || midiString.Contains("A2") || midiString.Contains("B2") || midiString.Contains("C2") || midiString.Contains("D2") || midiString.Contains("E2") || midiString.Contains("F2") || midiString.Contains("G2"))
                    {
                        if (track5[track5.Count - 1] >= (midiEvent.AbsoluteTime - 50))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(1220, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track5.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                }
            }
        }
    }
}
