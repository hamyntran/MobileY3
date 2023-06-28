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

public class TestAudioManager : MonoBehaviour
 {
    #region Variables
    
    #endregion
    
    #region Properties
    
    #endregion
    
    #region Constructor
    public TestAudioManager()
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
    
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(TestAudioManager))]
public class TestAudioManagerEditorCustom: Editor
{
	public override void OnInspectorGUI()
	{
		TestAudioManager _target = (TestAudioManager)target;
		DrawDefaultInspector();

		if (GUILayout.Button("Test Sound Effect"))
		{
		}
	}
}
#endif
