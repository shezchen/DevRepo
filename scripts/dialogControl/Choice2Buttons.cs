using UnityEngine;
using UnityEngine.UIElements;

public class Choice2Buttons : MonoBehaviour
{
    public static Choice2Buttons Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private UnityEngine.UI.Button button1;
    [SerializeField]private GameObject button1Text;
    [SerializeField]private UnityEngine.UI.Button button2;
    [SerializeField]private GameObject button2Text;
    
    
    public void Set2ChoiceButtons(string button1TextString,string button2TextString)
    {
        if(DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].state
           !=OneDialog.DialogState.Option)
            return;
        
        if(button1.gameObject.activeSelf==false)
        {
            button1.gameObject.SetActive(true);
        }

        if (button2.gameObject.activeSelf == false)
        {
            button2.gameObject.SetActive(true);
        }

        button1Text.GetComponent<TMPro.TextMeshProUGUI>().text = button1TextString;
        button2Text.GetComponent<TMPro.TextMeshProUGUI>().text = button2TextString;
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
        button1.onClick.AddListener(() =>
        {
            Debug.Log("Button 1 Clicked");
            dialogText.Instance.playerChoiceNumber = 1;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            
            button2.gameObject.SetActive(false);
            
        });
        button2.onClick.AddListener(() =>
        {
            Debug.Log("Button 2 Clicked");
            dialogText.Instance.playerChoiceNumber = 2;
            DialogPlayer.Instance.DialogPlayingIndex++;
            button1.gameObject.SetActive(false);
            
            button2.gameObject.SetActive(false);
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
