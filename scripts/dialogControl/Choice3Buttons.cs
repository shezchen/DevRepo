using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Choice3Buttons : MonoBehaviour
{
    public static Choice3Buttons Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private UnityEngine.UI.Button button1;
    [SerializeField] private GameObject button1Text;
    [SerializeField]private UnityEngine.UI.Button button2;
    [SerializeField] private GameObject button2Text;
    [SerializeField]private UnityEngine.UI.Button button3;
    [SerializeField] private GameObject button3Text;
    
    public void Set3ChoiceButtons(string button1TextString,string button2TextString,string button3TextString)
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button1Text.GetComponent<TMPro.TextMeshProUGUI>().text = button1TextString;
        button2Text.GetComponent<TMPro.TextMeshProUGUI>().text = button2TextString;
        button3Text.GetComponent<TMPro.TextMeshProUGUI>().text = button3TextString;
        button1.onClick.AddListener(() =>
        {
            
            Debug.Log("Button 1 Clicked");
            dialogText.Instance.playerChoiceNumber = 1;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
        });
        button2.onClick.AddListener(()  =>
        {
            Debug.Log("Button 2 Clicked");
            dialogText.Instance.playerChoiceNumber = 2;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
        });
        button3.onClick.AddListener(() =>
        {
            Debug.Log("Button 3 Clicked");
            dialogText.Instance.playerChoiceNumber = 3;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
