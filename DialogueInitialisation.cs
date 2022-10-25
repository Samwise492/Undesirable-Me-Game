using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class DialogueInitialisation
{
    public static Dictionary<int, string[]> dialogues = new Dictionary<int, string[]>();
    static string path = "Assets/Dialogues/";

    public static void Initialise()
    {
        // Scene 1
        StreamReader reader = new StreamReader(path + "Doctor_1.txt");
        dialogues.Add(1, reader.ReadToEnd().Split('\n'));

        reader = new StreamReader(path + "McKinsey_1.txt");
        dialogues.Add(2, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "McKinsey_1_1.txt");
        dialogues.Add(3, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "McKinsey_1_2.txt");
        dialogues.Add(4, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "McKinsey_1_3.txt");
        dialogues.Add(5, reader.ReadToEnd().Split('\n'));

        // Scene 2
        reader = new StreamReader(path + "Aubrey_2.txt");
        dialogues.Add(6, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "Aubrey_2_1.txt");
        dialogues.Add(7, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "Aubrey_2_2.txt");
        dialogues.Add(8, reader.ReadToEnd().Split('\n'));
        
        reader = new StreamReader(path + "knife_notice.txt");
        dialogues.Add(9, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "knife_choice.txt");
        dialogues.Add(10, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "suicide.txt");
        dialogues.Add(11, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "cancel_suicide.txt");
        dialogues.Add(12, reader.ReadToEnd().Split('\n'));

        // Scene 3
        reader = new StreamReader(path + "Doctor_3.txt");
        dialogues.Add(13, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "Doctor_3_1.txt");
        dialogues.Add(14, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "Doctor_3_2.txt");
        dialogues.Add(15, reader.ReadToEnd().Split('\n'));

        reader = new StreamReader(path + "good_ending.txt");
        dialogues.Add(16, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "bad_ending.txt");
        dialogues.Add(17, reader.ReadToEnd().Split('\n'));
        reader = new StreamReader(path + "chaotic_ending.txt");
        dialogues.Add(18, reader.ReadToEnd().Split('\n'));
    }
}
