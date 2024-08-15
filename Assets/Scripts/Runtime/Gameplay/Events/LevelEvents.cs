using System;

public static class LevelEvents
{
    public static Action<int> OnLevelSelected;
    public static Action OnLevelDataNeeded;
    public static Action<LevelScoresData[]> OnSpawnLevelSelectionButtons;
    public static Action<CompleteData> OnLevelWin;
}
