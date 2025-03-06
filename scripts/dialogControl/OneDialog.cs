using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class OneDialog
{
    [CanBeNull]public Background background;//TODO:背景控制
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
    
}
