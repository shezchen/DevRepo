namespace DefaultNamespace;

public class ReviewDialog
{
    public enum state
    {
        normal,
        narration,
        choice
    }
    public state dialogState = state.normal;
    public string speaker = "";
    public string sentence = "";
}