using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


/*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/
 
/// <summary>
/// 
/// </summary>

public class AudioManager : SingletonMonoBehaviour<AudioManager>
 {
    #region Variables

    [SerializeField] private AudioSource soundEfxSource, musicSource;
    
    public AudioClip soundEffect, backgroundMusic;

    
    #endregion
    
    #region Properties
    
    #endregion
    
    #region Constructor
    public AudioManager()
    {
    
    }
    #endregion
    
    #region Unity Callbacks
    private void Start()
    {
    
    }
    	
    private void Update()
    {
    
    }
    #endregion
    
    #region Methods

    public void PlaySoundEffect(AudioClip audioClip, float volume = 1, float pitch =1)
    {
	    soundEfxSource.clip = audioClip;
	    soundEfxSource.Play();
	    soundEfxSource.volume = volume;
	    soundEfxSource.pitch = pitch;
    }

    #endregion
 }


#if UNITY_EDITOR
[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditorCustom: Editor
{
	public override void OnInspectorGUI()
	{
		AudioManager _target = (AudioManager)target;
		DrawDefaultInspector();
		
		GUILayout.Space(15);

		if (GUILayout.Button("Test Sound Effect"))
		{
			_target.PlaySoundEffect(_target.soundEffect);
		}
	}
}
#endif