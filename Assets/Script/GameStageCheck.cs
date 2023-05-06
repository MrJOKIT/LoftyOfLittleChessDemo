using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStageCheck : MonoBehaviour
{
    public List<GameObject> doorSwitch;
    public List<GameObject> monster = new List<GameObject>();
    public DoorToNextStage doorToNextStage;

    private void Update()
    {

        if (monster.Count <= 0 )
        {
            if (doorSwitch.Count == 0)
            {
                doorToNextStage.isOpen = true;
            }
            else
            {
                DoorSwitch doorSwi = doorSwitch[0].GetComponent<DoorSwitch>();
                doorSwi.ready = true;
            }
            
        }
        else if (monster.Count > 0)
        {
            DoorSwitch doorSwi = doorSwitch[0].GetComponent<DoorSwitch>();
            doorSwi.ready = false;
        }
        else
        {
            return;
        }
        
    }

    public void AddMonster(GameObject addObject)
    {
        monster.Add(addObject);
    }

    public void RemoveMonster(GameObject removeObject)
    {
        monster.Remove(removeObject);
    }

    public void RemoveSwitch(GameObject switchObject)
    {
        doorSwitch.Remove(switchObject);
    }
}
