using System;

public static class LevelEvents
{
    public static Action<int> OnLevelSelected; // seçilen level için action 
    public static Action OnLevelDataNeeded; // Hangi levelin lazim olacaksa onu çağırmak için 
    public static Action<LevelScoresData[]> OnSpawnLevelSelectionButton; // burada ui a basıldığı zaman levelleri çağıracak bir action 
    public static Action<CompleteData> OnLevelWin; // leveli kaç puanla kazandığımızı ve kazanıp kazanmadığımızın actionı 
}
