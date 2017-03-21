using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public Popup m_MusicPopup;

	public AudioSource m_MusicSource;
	public AudioSource m_SoundEffectsSource;
	public AudioSource m_BreathingSource;
	public AudioMixer m_Mixer;

	public AudioProcessor m_AudioProcessor;

	private Object[] m_TechnoMusic;
	private Object[] m_MetalMusic;

	private Object[] m_Sounds;
	private Queue<AudioClip> m_Soundqueue;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else if (instance != this) {
			Destroy (gameObject);
		}

		m_Soundqueue = new Queue<AudioClip> ();
					
		m_AudioProcessor = GetComponent<AudioProcessor> ();
		m_TechnoMusic = Resources.LoadAll ("TechnoMusic", typeof(AudioClip));
		m_MetalMusic = Resources.LoadAll ("MetalMusic", typeof(AudioClip));
		m_Sounds = Resources.LoadAll ("Sounds", typeof(AudioClip));

		m_BreathingSource.clip = FindSound ("breathing", m_Sounds);
	}

	// Use this for initialization
	void Start () {
		if (GlobalConfig.STARTUP) {
			GlobalConfig.STARTUP = false;
			//Play (false, "Dustup");
			Play (false, "Thursday");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// If no music is playing, play random music 
		if (m_MusicSource.isPlaying == false) {
			Play (GlobalConfig.METAL_MODE, null);
		}

		// If current sound is finished, play the next in the queue 
		if (m_Soundqueue.Count > 0 && m_SoundEffectsSource.isPlaying == false) {
			m_SoundEffectsSource.clip = m_Soundqueue.Dequeue ();
			m_SoundEffectsSource.Play ();
		}
	}
		
	// Get sound by name
	private AudioClip FindSound(string name, Object[] sounds){
		int index = 0;

		for (int i = 0; i < sounds.Length; i++) {
			var clip = sounds [i] as AudioClip;
			if (clip.name.Contains (name)) {
				index = i;
				break;
			}
		}
		return sounds [index] as AudioClip;
	}

	// Plays a sound 
	public void Play(bool metal, string name){
		Object[] music;

		if (metal) {
			music = m_MetalMusic;
		} else {
			music = m_TechnoMusic;
		}

		AudioClip clip;

		if (name == null) {
			int index = Random.Range (0, music.Length);
			clip = music [index] as AudioClip;
		} else {
			clip = FindSound (name, music);
		}

		m_MusicSource.clip = clip;
		m_MusicSource.Play ();
		m_AudioProcessor.setAudioSource (m_MusicSource);

		// Show name in GUI
		string[] tokens = m_MusicSource.clip.name.Split ('-');

		m_MusicPopup.Show (tokens [0].Trim(), tokens [1].Trim(), 4);
	}
		
	// Plays a sound effect
	public void PlaySound(string name) {
		m_SoundEffectsSource.clip = FindSound(name, m_Sounds);
		m_SoundEffectsSource.Play ();
	}

	// Adds a effect to the soundQueue so the current one is not aborted
	public void PlaySoundQueue(string name){
		m_Soundqueue.Enqueue (FindSound(name,m_Sounds));
	}
		
	// Subscribe method for Beatlisteners 
	public void Listen(BeatListener bl){
		m_AudioProcessor.onBeat.AddListener (bl.OnBeat);
		m_AudioProcessor.onSpectrum.AddListener (bl.OnSpectrum);
	}

	// (De)Activates the theft-breathing
	public void SetBreathing(bool b){
		if (b) {
			m_BreathingSource.Play ();
		} else {
			m_BreathingSource.Pause ();
		}
	}

	public void ClearQueue(){
		m_SoundEffectsSource.Stop ();
		m_Soundqueue.Clear ();
	}


}
