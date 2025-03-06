using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Choice2Buttons : MonoBehaviour
{
    public static Choice2Buttons Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private UnityEngine.UI.Button button1;
    [SerializeField] private GameObject button1Text;
    [SerializeField]private UnityEngine.UI.Button button2;
    [SerializeField] private GameObject button2Text;
    
    public void Set2ChoiceButtons(string button1TextString,string button2TextString)
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button1Text.GetComponent<TMPro.TextMeshProUGUI>().text = button1TextString;
        button2Text.GetComponent<TMPro.TextMeshProUGUI>().text = button2TextString;
        button1.onClick.AddListener(() =>
        {
            
            Debug.Log("Button 1 Clicked");
            DialogPlayer.Instance.PlayedDialogDatabase.Add(new ReviewDialog()
            {
                state = ReviewDialog.state.choice,
                speaker = "",
                sentence = button1TextString
            });
            dialogText.Instance.playerChoiceNumber = 1;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
        });
        button2.onClick.AddListener(()  =>
        {
            Debug.Log("Button 2 Clicked");
            DialogPlayer.Instance.PlayedDialogDatabase.Add(new ReviewDialog()
            {
                state = ReviewDialog.state.choice,
                speaker = "",
                sentence = button2TextString
            });
            dialogText.Instance.playerChoiceNumber = 2;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
        });
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }
}
