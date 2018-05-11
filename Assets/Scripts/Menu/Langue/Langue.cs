using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class Langue {

	public static string path;
	public static Texts textes;

    public static void LoadPC() {
        string json = File.ReadAllText(path);
        textes = JsonUtility.FromJson<Texts>(json);
    }
	public static IEnumerator LoadAndroid (string path) {
		WWW www = new WWW (path);
		yield return www;
		textes = JsonUtility.FromJson<Texts> (www.text);
	}

	public static void Create () {
		Texts t = new Texts ();
		string json = JsonUtility.ToJson (t, true);
		File.WriteAllText (path, json);
	}

	public static string GetValue (string name) {
		try {
			return textes.GetType ().GetField (name).GetValue (textes).ToString ();
		} catch {
			return null;
		}
	}
}