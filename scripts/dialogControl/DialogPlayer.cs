using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogPlayer : MonoBehaviour
{
    // Singleton instance
    public static DialogPlayer Instance { get; private set; }
    [SerializeField] public UnityEngine.UI.Button myButton;
    
    
    [SerializeField] public Image BackgroundImage;
    [SerializeField] public Image SpeakerImage;
    public bool isOptionVisible;


    [SerializeField] private const int ReadingSpeedFrame = 10;
    [SerializeField] private const int ReadingSpeedFrameFast = 4;
    private static int CallCount;
    [NotNull] private static string PreSentence = "";//之前的完整字符串
    [NotNull] public static string OutSentence = "";//正在输出到屏幕的字符串

    public int DialogPlayingIndex;

    public static string GenDialogString(string sentence)
    {
        //针对每一句对话生成要显示的字符串
        if(sentence == null)
        {
            Debug.Log("sentence is null");
            return "";
        }

        if (sentence != PreSentence)
        {
            //new sentence
            CallCount = 0;
        }
        else
        {
            CallCount++;
        }
        int playIndex = CallCount / ReadingSpeedFrame;
        if(playIndex >= sentence.Length)
        {
            playIndex = sentence.Length - 1;
        }
        PreSentence = sentence;
        if(OutSentence != sentence)
            OutSentence = sentence[0 .. (playIndex+1)] ?? "";
        return OutSentence;
    }

    [CanBeNull] public OneDialog[] DialogDatabase;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the instance across scenes
        }
        else
        {
            Destroy(gameObject);
        }
        
        DialogDatabase = null;//初始设置为null
        DialogPlayingIndex = 0;//初始设置为0
    }
    [SerializeField] Canvas DialogCanvas;
    public bool isDialogPlaying=false;
    
    public void StartDialogPlay(string targetFileName)
    {
        DialogFileReader.ReadDialogFile(out DialogDatabase, targetFileName);
        DialogPlayingIndex = 0;
        isDialogPlaying = true;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    private bool previousMouseState = false;
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !previousMouseState)
        {
            if (DialogDatabase == null)
            {
                return;
            }

            if (OutSentence.Length < DialogDatabase[DialogPlayingIndex].Sentence.Length)
            {
                OutSentence = DialogDatabase[DialogPlayingIndex].Sentence;
            }
            else
            {
                if(DialogDatabase[DialogPlayingIndex].state != OneDialog.DialogState.Option)
                {
                    DialogPlayingIndex++;
                    if (DialogPlayingIndex >= DialogDatabase.Length)
                    {
                        DialogCanvas.gameObject.SetActive(false); 
                        DialogPlayer.Instance.isDialogPlaying = false;
                        GameStateControl.Instance.SetGameState(GameStateControl.GameState.Menu);
                        myButton.gameObject.SetActive(true);
                    }
                }
            }
        }
        previousMouseState = Input.GetMouseButtonDown(0);
    }
}
