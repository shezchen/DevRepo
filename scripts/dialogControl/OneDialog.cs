using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class OneDialog
{
    [CanBeNull]public Background background;
    public string BackgroundName="";

    public enum DialogState
    {
        Init,
        Normal,
        NormalChoice,
        Narration,
        NarrationChoice,
        Background,
        Option
    }
    
    public DialogState state=DialogState.Init;

    public int NeedOption;
    
    public string Speaker="";
    
    public string Sentence="";

    public string SpeakerIcon="";
    
    [CanBeNull]public OneDialog[] OptionDialogs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
