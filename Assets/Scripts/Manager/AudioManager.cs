using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public Popup m_MusicPopup;

	public AudioProcessor m_AudioProcessor;
	private Object[] m_MetalMusic;
	private AudioSource m_AudioSource;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		m_MetalMusic = Resources.LoadAll ("MetalMusic", typeof(AudioClip));
		Debug.Log ("Loaded");
		Debug.Log (m_MetalMusic.Length);
	}

	// Use this for initialization
	void Start () {
		m_AudioSource = GetComponent<AudioSource> ();
		m_AudioProcessor = GetComponent<AudioProcessor> ();

		m_MusicPopup.Show ("Header", "MainText", 5);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_AudioSource.isPlaying == false) {
			//if (GlobalConfig.METAL_MODE) {
				PlayMetal ();
			//} else {
			//}
		}
	}
		
	public void PlayMetal(){
		int rand = Random.Range (0, m_MetalMusic.Length);
		m_AudioSource.clip = m_MetalMusic [rand] as AudioClip;
		m_AudioSource.Play ();
		m_AudioProcessor.setAudioSource (m_AudioSource);

		// Show name in GUI
		string[] tokens = m_AudioSource.clip.name.Split ('-');

		m_MusicPopup.Show (tokens [0].Trim(), tokens [1].Trim(), 4);
	}

	public void PlayStandart(){

	}
}
