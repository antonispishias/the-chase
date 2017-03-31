using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {

	public SoundGroup[] soundGroups;
	Dictionary<string, AudioClip[]> groupDict = new Dictionary<string, AudioClip[]>();


	void Awake () {
		foreach (SoundGroup sg in soundGroups) {
			groupDict.Add(sg.groupID,sg.group);
		} 
	}

	public AudioClip GetClipFromName(string name) {
		if (groupDict.ContainsKey (name)) {
			AudioClip[] sounds = groupDict [name];
			return sounds [Random.Range (0, sounds.Length)];
		}
		return null;
	}

	[System.Serializable]
	public class SoundGroup {
		public string groupID;
		public AudioClip[] group;
	}
}
