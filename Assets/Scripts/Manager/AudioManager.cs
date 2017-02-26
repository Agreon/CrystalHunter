using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public Popup m_MusicPopup;

	public AudioProcessor m_AudioProcessor;
	private Object[] m_TechnoMusic;
	private Object[] m_MetalMusic;
	private AudioSource m_AudioSource;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

	//	DontDestroyOnLoad (gameObject);
		m_AudioSource = GetComponent<AudioSource> ();
		m_AudioProcessor = GetComponent<AudioProcessor> ();
		m_TechnoMusic = Resources.LoadAll ("TechnoMusic", typeof(AudioClip));
		m_MetalMusic = Resources.LoadAll ("MetalMusic", typeof(AudioClip));
	}

	// Use this for initialization
	void Start () {
		m_MusicPopup.Show ("Header", "MainText", 5);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_AudioSource.isPlaying == false) {
			if (GlobalConfig.METAL_MODE) {
				Play (false, null);
			} else {
				Play (true, null);
			}
		}
	}


	public void Play(bool standart, string name){

		Object[] music;

		if (standart) {
			music = m_TechnoMusic;
		} else {
			music = m_MetalMusic;
		}

		int index = 0;

		if (name == null) {
			index = Random.Range (0, music.Length);
		} else {
			for (int i = 0; i < music.Length; i++) {
				var clip = music [i] as AudioClip;
				if (clip.name.Contains (name)) {
					index = i;
					break;
				}
			}
		}

		m_AudioSource.clip = music [index] as AudioClip;
		m_AudioSource.Play ();
		m_AudioProcessor.setAudioSource (m_AudioSource);

		// Show name in GUI
		string[] tokens = m_AudioSource.clip.name.Split ('-');

		m_MusicPopup.Show (tokens [0].Trim(), tokens [1].Trim(), 4);
	}
}
