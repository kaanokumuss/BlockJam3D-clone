[System.Serializable]
public struct LevelScoresData 
{
    //logic barindirmaz class açmamızın sebebi daha optime 
    public int index;
    public string title;
    public int highScore;
    public bool isUnlocked;
    // structlarla veri tutuyoruz ..
}
