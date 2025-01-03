namespace LangVoyageServer.Requests;

public class NounProgressRequest
{
    public int NounId { get; set; }
    public bool AnswerWasCorrect { get; set; }

}