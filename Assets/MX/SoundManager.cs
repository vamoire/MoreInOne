using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MX
{
	/// <summary>
	/// 声音管理
	/// </summary>
	public class SoundManager : Instance<SoundManager> {

		GameObject gameObject = null;
		SoundAutoRemove soundAutoRemove = null;

		AudioSource bgMusic = null;

		const string Key_Music = "SoundManager_Music";
		const string Key_Effect = "SoundManager_Effect";

		public bool bgmEnabled {
			get {
				AudioSource music = GetMusic();
				return !music.mute;
			}
			set {
				AudioSource music = GetMusic();
				music.mute = !value;
				PlayerPrefs.SetInt(Key_Music, value ? 1 : 0);
			}
		}
		bool _effectEnabled = true;
		public bool effectEnabled {
			get {
				return _effectEnabled;
			}
			set {
				_effectEnabled = value;
				PlayerPrefs.SetInt(Key_Effect, value ? 1 : 0);
				AudioListener.pause = !value;
			}
		}

		public SoundManager() {
			//通用声音节点
			gameObject = new GameObject("Sound");
			GameObject.DontDestroyOnLoad(gameObject);
			soundAutoRemove = gameObject.AddComponent<SoundAutoRemove>();
			//bgm
			bgmEnabled = PlayerPrefs.GetInt(Key_Music, 1) == 1;
			//effect
			effectEnabled = PlayerPrefs.GetInt(Key_Effect, 1) == 1;
		}

		AudioSource CreateSound(GameObject obj = null) {
			if (obj == null) {
				obj = gameObject;
			}
			var audioSource = obj.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.loop = false;
			return audioSource;
		}

		AudioClip SoundPlay(AudioSource sound, string name) {
			var clip = Resources.Load(name, typeof(AudioClip)) as AudioClip;
			if (clip) {
				sound.clip = clip;
			}
			sound.Play();
			return clip;
		}

		AudioSource GetMusic() {
			if (bgMusic == null) {
				bgMusic = CreateSound();
				bgMusic.loop = true;
				bgMusic.ignoreListenerPause = true;
				bgMusic.ignoreListenerVolume = true;
			}
			return bgMusic;
		}

		public void PlayMusic(string name) {
			AudioSource music = GetMusic();
			if (music.clip == null || music.isPlaying == false || music.clip.name != name) {
				SoundPlay(music, name);
			}
		}

		public void StopMusic() {
			AudioSource music = GetMusic();
			music.clip = null;
		}

		public AudioSource PlayeEffect(string name, bool loop = false, GameObject obj = null) {
			AudioSource audioSource = CreateSound(obj);
			audioSource.loop = loop;
			SoundPlay(audioSource, name);

			soundAutoRemove.AddSound(audioSource);
			
			return audioSource;
		}

		public void StopEffect(AudioSource sound) {
			GameObject.Destroy(sound);
		}

		public void StopAllEffect() {
			soundAutoRemove.RemoveAllSound();
		}

	}

	public class SoundAutoRemove : MonoBehaviour {
		List<AudioSource> list = new List<AudioSource>();

		public void AddSound(AudioSource audioSource) {
			list.Add(audioSource);
		}

		public void RemoveAllSound() {
			foreach (var item in list)
			{
				Destroy(item);
			}
			list.Clear();
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			list.RemoveAll((AudioSource sound)=>{
				if (sound && sound.isPlaying == false) {
					Destroy(sound);
				}
				return sound == null || sound.isPlaying == false;
			});
		}
	}
}

