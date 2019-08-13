using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuControl : MonoBehaviour
{
    private Transform menuPanel;
    private Event keyEvent;
    private TMP_Text buttonText;
    private KeyCode newKey;

    private bool waitingForKey;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        menuPanel = transform.Find("Controls");
        waitingForKey = false;

        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "ForwardKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.forward.ToString();
            }
            if (menuPanel.GetChild(i).name == "BackwardKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.backward.ToString();
            }
            if (menuPanel.GetChild(i).name == "LeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.left.ToString();
            }
            if (menuPanel.GetChild(i).name == "RightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.right.ToString();
            }
            if (menuPanel.GetChild(i).name == "JumpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.jump.ToString();
            }
        }
    }

    //Get the key pressed
    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    //Assign the key on the coroutine
    public void StartAssigment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    //Change the key text
    public void SendText(TMP_Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }
    
    //Assign the key for each case
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
           case "forward":
               GameManager.GM.forward = newKey;
               buttonText.text = GameManager.GM.forward.ToString();
               PlayerPrefs.SetString("forwardKey", GameManager.GM.forward.ToString());
               break;
           case "backward":
               GameManager.GM.backward = newKey;
               buttonText.text = GameManager.GM.backward.ToString();
               PlayerPrefs.SetString("backwardKey", GameManager.GM.backward.ToString());
               break;
           case "left":
               GameManager.GM.left = newKey;
               buttonText.text = GameManager.GM.left.ToString();
               PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString());
               break;
           case "right":
               GameManager.GM.right = newKey;
               buttonText.text = GameManager.GM.right.ToString();
               PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString());
               break;
           case "jump":
               GameManager.GM.jump = newKey;
               buttonText.text = GameManager.GM.jump.ToString();
               PlayerPrefs.SetString("jumpKey", GameManager.GM.jump.ToString());
               break;
        }

        yield return null;
    }
}
