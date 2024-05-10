using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BulletEcho.SoundSystem
{
     [Serializable]
    public enum SoundType
    {
        Gun,
        PickUp,
        HeathUp
    }

    [Serializable]
    public class SoundRef
    {
        public SoundType _SoundType;
        public AudioClip _AudioClip;
    }
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get;private set; }
        [FormerlySerializedAs("m_soundRefs")]
        [Header("External Dependency")]
        [SerializeField] private SoundRef[] soundRefs;
        [SerializeField] private AudioSource audioPrefab;
        [SerializeField] private AudioClip bgSound;
        [SerializeField] private AudioSource bgAudioSource;
        private Dictionary<SoundType, AudioSource> soundSources;
        private bool mMuteAudio;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Instance.Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            soundSources= new Dictionary<SoundType, AudioSource>();
            var thisTrans = transform;
            for (int i = 0; i < soundRefs.Length; i++)
            {
                var data = soundRefs[i];
                var gameObj=Instantiate(audioPrefab.gameObject,thisTrans);
                gameObj.name = data._SoundType.ToString();
                gameObj.transform.parent = thisTrans;
                var audioSource = gameObj.GetComponent<AudioSource>();
                audioSource.clip = data._AudioClip;
                soundSources.Add(data._SoundType,audioSource);
            }
        }
        public void PlaySoundType(SoundType soundType)
        {
            if(mMuteAudio)
                return;
            if (soundSources.ContainsKey(soundType))
            {
                var audioSource=soundSources[soundType];
                    if(audioSource.isPlaying)
                        audioSource.Stop();
                    audioSource.Play();
            }
            else
            {
#if DEBUG_LOG
                Debug.LogError($"TAG::SoundManager || Sound Type <color=red>{soundType.ToString()} </color> is not added to sound Manager");
#endif
            }
        }

        public void MuteAll()
        {
            mMuteAudio = true;
            foreach (var audioItem in soundSources)
            {
                var audioSource = audioItem.Value;
                if(audioSource.isPlaying)
                    audioSource.Stop();
            }
            bgAudioSource.Stop();
        }
    }
}