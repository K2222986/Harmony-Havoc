using UnityEngine;
using NAudio.Midi;
using NAudio;
using System;
using System.Linq;

public class MidiScript : MonoBehaviour
{
    
    public string fileName = "Assets/MIDI/londons-burning.mid";
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
                    Debug.Log("{0} {1}\r\n" + ToMBT(midiEvent.AbsoluteTime, mf.DeltaTicksPerQuarterNote, timeSignature) + " " + midiEvent);
                    NoteScript newNote = Instantiate(finalObject, new Vector3(400, 500 + ((midiEvent.AbsoluteTime / 768) * 500), -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                }
            }
        }
    }
}
