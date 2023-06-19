using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameDetection : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentState != GameManager.GameState.Begin) {return;}

        if (Input.GetMouseButtonDown(0)
            && EventSystem.current.currentSelectedGameObject == null && !PanelOpen())
        {
            GameManager.OnSwitchState?.Invoke( GameManager.GameState.InGame);
            
            Debug.Log("switchhhhhh");
        }
    }

    private bool PanelOpen()
    {
        foreach (GameObject p in panels)
        {
            if (p.activeSelf)
            {
                return true;
            }
        }
        
        return false;
    }
}
