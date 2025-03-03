using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialogText : MonoBehaviour
{
    public static dialogText Instance {get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int playerChoiceNumber = 0;//当前玩家选择的选项
    [SerializeField] private GameObject targetSpeakerObject;
    [SerializeField] private GameObject targetTextObject;
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
        
        if(targetTextObject!=null)
        {
            Debug.Log("Found the target object");
            targetTextObject.GetComponent<TextMeshProUGUI>().text = "dialog";
        }
        if(targetSpeakerObject!=null)
        {
            Debug.Log("Found the target object");
            targetSpeakerObject.GetComponent<TextMeshProUGUI>().text = "Speaker";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (DialogPlayer.Instance?.isDialogPlaying ?? false)
        {
            string sentence = "";
            string speaker = "";
            switch (DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].state)
            {
                case OneDialog.DialogState.Normal:
                    
                    sentence = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                        .Sentence;
                    targetTextObject.GetComponent<TextMeshProUGUI>().text = DialogPlayer.GenDialogString(sentence);
                    speaker = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                        .Speaker;
                    targetSpeakerObject.GetComponent<TextMeshProUGUI>().text = speaker;
                    break;
                
                case OneDialog.DialogState.NormalChoice:

                    if (DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].NeedOption !=
                        playerChoiceNumber)
                    {
                        DialogPlayer.Instance.DialogPlayingIndex++;
                        break;
                    }

                    sentence = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                        .Sentence;
                    targetTextObject.GetComponent<TextMeshProUGUI>().text = DialogPlayer.GenDialogString(sentence);
                    speaker = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                        .Speaker;
                    targetSpeakerObject.GetComponent<TextMeshProUGUI>().text = speaker;
                    break;
                
                case OneDialog.DialogState.Narration:
                    
                    sentence = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                        .Sentence;
                    targetTextObject.GetComponent<TextMeshProUGUI>().text = DialogPlayer.GenDialogString(sentence);
                    speaker = "";
                    targetSpeakerObject.GetComponent<TextMeshProUGUI>().text = speaker;
                    break;
                
                case OneDialog.DialogState.NarrationChoice:

                    if (DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].NeedOption !=
                        playerChoiceNumber)
                    {
                        DialogPlayer.Instance.DialogPlayingIndex++;
                        break;
                    }

                    sentence = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                        .Sentence;
                    targetTextObject.GetComponent<TextMeshProUGUI>().text = DialogPlayer.GenDialogString(sentence);
                    speaker = "";
                    targetSpeakerObject.GetComponent<TextMeshProUGUI>().text = speaker;
                    break;
                
                case OneDialog.DialogState.Background:
                    //TODO: Background
                    DialogPlayer.Instance.DialogPlayingIndex++;
                    break;
                case OneDialog.DialogState.Option:
                    
                    sentence = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex - 1]
                        .Sentence;
                    targetTextObject.GetComponent<TextMeshProUGUI>().text = sentence;
                    speaker = DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex - 1]
                        .Speaker;
                    targetSpeakerObject.GetComponent<TextMeshProUGUI>().text = speaker;
                    if (DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].OptionDialogs
                            .Length == 2)
                    {
                        //2 choice
                        Choice2Buttons.Instance.Set2ChoiceButtons(
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[0].Sentence,
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[1].Sentence);
                        
                    }else if (DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].OptionDialogs
                        .Length == 3)
                    {
                        //3 choice
                        Choice3Buttons.Instance.Set3ChoiceButtons(
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[0].Sentence,
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[1].Sentence,
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[2].Sentence);
                        
                    }else if (DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex].OptionDialogs
                        .Length == 4)
                    {
                        //TODO:4 choice
                        /*Choice4Buttons.Set4ChoiceButtons(
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[0].Sentence,
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[1].Sentence,
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[2].Sentence,
                            DialogPlayer.Instance.DialogDatabase[DialogPlayer.Instance.DialogPlayingIndex]
                                .OptionDialogs[3].Sentence);*/
                        
                    }
                    
                    
                    break;
            }

            

            

        }
    }
}
