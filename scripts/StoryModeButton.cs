using System;
using UnityEngine;
using UnityEngine.UI;

public class StoryModeButton : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public UnityEngine.UI.Button myButton;
    [SerializeField] private Canvas DialogCanvas;
    void Start()
    {
        if(myButton != null)
        {
            myButton.onClick.AddListener(() => {
                Debug.Log("Button Clicked");
               
                DialogCanvas.gameObject.SetActive(true); 
                DialogPlayer.Instance.isDialogPlaying = true;
                GameStateControl.Instance.SetGameState(GameStateControl.GameState.Story);
                myButton.gameObject.SetActive(false);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
