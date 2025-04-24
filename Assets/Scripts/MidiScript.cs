using UnityEngine;
using NAudio.Midi;
using NAudio;
using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MidiScript : MonoBehaviour
{
    public List<int> track1 = new List<int>();
    public List<int> track2 = new List<int>();
    public List<int> track3 = new List<int>();
    public List<int> track4 = new List<int>();
    public List<int> track5 = new List<int>();
    public int[] noteCountArray = new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] noteNameArray = new string[]{"A0", "A#0", "B0", "C1", "C#1", "D1", "D#1", "E1", "F1", "F#1", "G1", "G#1",
 "A1", "A#1", "B1", "C2", "C#2", "D2", "D#2", "E2", "F2", "F#2", "G2", "G#2",
 "A2", "A#2", "B2", "C3", "C#3", "D3", "D#3", "E3", "F3", "F#3", "G3", "G#3",
 "A3", "A#3", "B3", "C4", "C#4", "D4", "D#4", "E4", "F4", "F#4", "G4", "G#4",
 "A4", "A#4", "B4", "C5", "C#5", "D5", "D#5", "E5", "F5", "F#5", "G5", "G#5",
 "A5", "A#5", "B5", "C6", "C#6", "D6", "D#6", "E6", "F6", "F#6", "G6", "G#6",
 "A6", "A#6", "B6", "C7", "C#7", "D7", "D#7", "E7", "F7", "F#7", "G7", "G#7",
 "A7", "A#7", "B7", "C8"};
    public string fileName = "Assets/MIDI/rush_e_real.mid";
    public string midiString;
    private int tempInt;
    private string tempString;
    private bool sorted = false;
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
        track1.Add(-1);
        track2.Add(-1);
        track3.Add(-1);
        track4.Add(-1);
        track5.Add(-1);
        foreach (var midiEvent in mf.Events[1])
        {
            if (!MidiEvent.IsNoteOff(midiEvent))
            {
                midiString = midiEvent.ToString();
                for (int n = 0; n < noteNameArray.Length; n++)
                {
                    //Debug.Log(midiString);
                    //Debug.Log(noteNameArray[n]);
                    if (midiString.Contains(noteNameArray[n]))
                    {
                        noteCountArray[n]++;
                        //Debug.Log(noteNameArray[n]);
                        break;
                    }
                }
            }
        }

        while (!sorted)
        {
            sorted = true;
            for (int n = 0; n < noteCountArray.Length - 1; n++)
            {
                if (noteCountArray[n] > noteCountArray[n + 1])
                {
                    tempInt = noteCountArray[n + 1];
                    tempString = noteNameArray[n + 1];
                    noteCountArray[n + 1] = noteCountArray[n];
                    noteCountArray[n] = tempInt;
                    noteNameArray[n + 1] = noteNameArray[n];
                    noteNameArray[n] = tempString;
                    sorted = false;
                }
            }
        }

        //for (int n = 0; n < mf.Tracks; n++)
        {
            foreach (var midiEvent in mf.Events[1])
            {
                if (!MidiEvent.IsNoteOff(midiEvent))
                {
                    midiString = midiEvent.ToString();
                    if (midiString.Contains(noteNameArray[0]) ||
midiString.Contains(noteNameArray[5]) ||
midiString.Contains(noteNameArray[10]) ||
midiString.Contains(noteNameArray[15]) ||
midiString.Contains(noteNameArray[20]) ||
midiString.Contains(noteNameArray[25]) ||
midiString.Contains(noteNameArray[30]) ||
midiString.Contains(noteNameArray[35]) ||
midiString.Contains(noteNameArray[40]) ||
midiString.Contains(noteNameArray[45]) ||
midiString.Contains(noteNameArray[50]) ||
midiString.Contains(noteNameArray[55]) ||
midiString.Contains(noteNameArray[60]) ||
midiString.Contains(noteNameArray[65]) ||
midiString.Contains(noteNameArray[70]) ||
midiString.Contains(noteNameArray[75]) ||
midiString.Contains(noteNameArray[80]) ||
midiString.Contains(noteNameArray[85]))
                    {
                        if (track1[track1.Count - 1] < (midiEvent.AbsoluteTime - 120))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(700, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track1.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }

                    }
                    else if (midiString.Contains(noteNameArray[1]) ||
midiString.Contains(noteNameArray[6]) ||
midiString.Contains(noteNameArray[11]) ||
midiString.Contains(noteNameArray[16]) ||
midiString.Contains(noteNameArray[21]) ||
midiString.Contains(noteNameArray[26]) ||
midiString.Contains(noteNameArray[31]) ||
midiString.Contains(noteNameArray[36]) ||
midiString.Contains(noteNameArray[41]) ||
midiString.Contains(noteNameArray[46]) ||
midiString.Contains(noteNameArray[51]) ||
midiString.Contains(noteNameArray[56]) ||
midiString.Contains(noteNameArray[61]) ||
midiString.Contains(noteNameArray[66]) ||
midiString.Contains(noteNameArray[71]) ||
midiString.Contains(noteNameArray[76]) ||
midiString.Contains(noteNameArray[81]) ||
midiString.Contains(noteNameArray[86]))
                    {
                        if (track2[track2.Count - 1] < (midiEvent.AbsoluteTime - 120))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(830, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track2.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                    else if (midiString.Contains(noteNameArray[2]) ||
midiString.Contains(noteNameArray[7]) ||
midiString.Contains(noteNameArray[12]) ||
midiString.Contains(noteNameArray[17]) ||
midiString.Contains(noteNameArray[22]) ||
midiString.Contains(noteNameArray[27]) ||
midiString.Contains(noteNameArray[32]) ||
midiString.Contains(noteNameArray[37]) ||
midiString.Contains(noteNameArray[42]) ||
midiString.Contains(noteNameArray[47]) ||
midiString.Contains(noteNameArray[52]) ||
midiString.Contains(noteNameArray[57]) ||
midiString.Contains(noteNameArray[62]) ||
midiString.Contains(noteNameArray[67]) ||
midiString.Contains(noteNameArray[72]) ||
midiString.Contains(noteNameArray[77]) ||
midiString.Contains(noteNameArray[82]) ||
midiString.Contains(noteNameArray[87]))
                    {
                        if (track3[track3.Count - 1] < (midiEvent.AbsoluteTime - 120))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(960, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track3.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                    else if (midiString.Contains(noteNameArray[3]) ||
midiString.Contains(noteNameArray[8]) ||
midiString.Contains(noteNameArray[13]) ||
midiString.Contains(noteNameArray[18]) ||
midiString.Contains(noteNameArray[23]) ||
midiString.Contains(noteNameArray[28]) ||
midiString.Contains(noteNameArray[33]) ||
midiString.Contains(noteNameArray[38]) ||
midiString.Contains(noteNameArray[43]) ||
midiString.Contains(noteNameArray[48]) ||
midiString.Contains(noteNameArray[53]) ||
midiString.Contains(noteNameArray[58]) ||
midiString.Contains(noteNameArray[63]) ||
midiString.Contains(noteNameArray[68]) ||
midiString.Contains(noteNameArray[73]) ||
midiString.Contains(noteNameArray[78]) ||
midiString.Contains(noteNameArray[83]))
                    {
                        if (track4[track4.Count - 1] < (midiEvent.AbsoluteTime - 120))
                        {
                            NoteScript newNote = Instantiate(finalObject, new Vector3(1090, 190 + midiEvent.AbsoluteTime, -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                            track4.Add(Convert.ToInt32(midiEvent.AbsoluteTime));
                        }
                    }
                    else if (midiString.Contains(noteNameArray[4]) ||
midiString.Contains(noteNameArray[9]) ||
midiString.Contains(noteNameArray[14]) ||
midiString.Contains(noteNameArray[19]) ||
midiString.Contains(noteNameArray[24]) ||
midiString.Contains(noteNameArray[29]) ||
midiString.Contains(noteNameArray[34]) ||
midiString.Contains(noteNameArray[39]) ||
midiString.Contains(noteNameArray[44]) ||
midiString.Contains(noteNameArray[49]) ||
midiString.Contains(noteNameArray[54]) ||
midiString.Contains(noteNameArray[59]) ||
midiString.Contains(noteNameArray[64]) ||
midiString.Contains(noteNameArray[69]) ||
midiString.Contains(noteNameArray[74]) ||
midiString.Contains(noteNameArray[79]) ||
midiString.Contains(noteNameArray[84]))
                    {
                        if (track5[track5.Count - 1] < (midiEvent.AbsoluteTime - 120))
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
