/* This class plays sound effects when called.
 * 
 * When you want to add a sound effect, add the following line to the triggering event script
 * (with "SoundNameHere" changed to name of the Sound object):
   FindObjectOfType<TalkPlayer>().Play("SoundNameHere");
 */

using UnityEngine.Audio;
using UnityEngine;
using System;

public class TalkPlayer : MonoBehaviour {

	public Sound[] sounds;
	public bool talkEnabled;
    public bool debugNotes;

	//public static TalkPlayer instance;

	private AudioSource current;
	private AudioSource pause;

	// Scrolls through the list of sounds when invoken, and sets the sound settings
	void Awake() {
		// This commented out section is not needed due there being only one scene anyway.

		/*
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		//DontDestroyOnLoad (gameObject);
		*/

		if (sounds != null) {
			foreach(Sound s in sounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;

				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				// print (s.name);
			}
		}

		current = GameObject.Find("AI Talker Speech").GetComponent<AudioSource>();
		//AddComponent<AudioSource> ();
		pause = GameObject.Find("AI Talker Static").GetComponent<AudioSource>();
		//Camera.main.gameObject.AddComponent<AudioDistortionFilter> ();
		//Camera.main.gameObject.AddComponent<AudioReverbFilter> ();

		Sound n = Array.Find (this.sounds, sound => sound.name.Equals("pause"));
		if (n != null) {
			pause.clip = n.clip;
			pause.volume = n.volume;
		} else {
            notify("Warning: Pause clip not found.", true);
		}

	}

	void Start() {
		
	}

	void Update()
    {
        // Checks if talk is to be set enabled.
        if (Input.GetKeyDown(KeyCode.P))
        {
            talkEnabled = !talkEnabled;
            notify("Talk is enabled: " + talkEnabled);
        }

        if (!talkEnabled && current.isPlaying) {
            current.Stop();
        }
	}

	// Plays a sound according to the set name
	public void Play(string name) {
		//Debug.Log (talkEnabled + " ja " + name);
		if (talkEnabled) {
			if (sounds != null) {
				Sound s = Array.Find (sounds, sound => sound.name == name);
				if (s != null) {
					if (current != null) {
						current.Stop ();
					}
					if (pause != null) {
						pause.Play ();
					}

					current.clip = s.clip;
					current.PlayDelayed (1.2f);
                    notify("TalkPlayer plays: " + name + " at volume " + s.volume);
				} else {
                    notify("Sound '" + name + "' not found!", true);
				}
			} else {
                notify("Sound list not found!", true);
			}
		}
	}

	/**
	 * Plays a random sound from the sound list, assuming there are more than one sound of the same type.
	 * Expected that there are multiple sounds with names Sound1, Sound2, Sound3 etc.,
	 * in which case the function can be called with parameters ("Sound", 3), which will cause
	 * any of the first three sounds to be played.
	 * Sound numberings in names are expected to start from 1.
	 */
	public void Play(string name, int rnd) {
		if (rnd > 0) {
			int seq = new System.Random().Next(1,rnd + 1);
			System.Object checkup = Array.Find (sounds, sound => sound.name == name + seq);
			//print (seq + " searcing: " + checkup);
			if (checkup != null) {
				Play (name + seq);
				//Debug.Log ("Should play: " + name + seq);
			} else {
				while(seq > 0 && checkup == null) {
					checkup = Array.Find (sounds, sound => sound.name == name + seq);
					if (checkup != null) {
						Play (name + seq);
					}
					//print (seq + " resuming: " + checkup);
					seq -= 1;
				}
			}
		} else {
			Play (name, 4);
            notify("Warning: Randomized sound was set illegaly.");
		}
	}

	public bool isPlaying() {
		return current.isPlaying;
	}

	public float currentLength() {
		return current.clip.length;
	}

    // Basic notification
    private void notify(string text) {
        if (debugNotes && text != null) {
            notify(text, false);
        }
    }

    // Notification with warning feature
    private void notify(string text, bool warning) {
        if (debugNotes && text != null) {
            if (!warning) {
                Debug.Log(text);
            } else {
                Debug.LogWarning(text);
            }
        }
    }
}
