//bitirilen level in scoreu ve indexi 

using UnityEditor.Build;

public struct CompleteData
{
    public int Score;
    public int Index;

    public CompleteData(int index, int score)
    {
        Index = index;
        Score = score;
    }
}
