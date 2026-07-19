using Monocle;
using TowerFall;

namespace TFModFortRiseGameModeRespawn
{
  /// <summary>
  /// Per-player handicap configured from the versus match settings screen.
  /// Victory handicap adds starting score (skulls/coins on round results).
  /// Lives handicap adds extra lives in multi-life modes.
  /// </summary>
  public static class PlayerHandicap
  {
    public const int MaxImmunityHandicap = 10; 
    //public const int MaxLivesHandicap = 1000000000;  //todo train
    public const int MaxLivesHandicap = 10;  //todo train

    private static int ImmunityHandicap = 0;
    private static readonly int[] LivesHandicap = { 1, 1, 1, 1, 1, 1, 1, 1 }; //todo train
    //private static readonly int[] LivesHandicap = { MaxLivesHandicap, MaxLivesHandicap, MaxLivesHandicap, MaxLivesHandicap, MaxLivesHandicap, MaxLivesHandicap, MaxLivesHandicap, MaxLivesHandicap };  //todo train

    public static int GetImmunityHandicap()
    {
      return ImmunityHandicap;
    }

    public static int GetLivesHandicap(int playerIndex)
    {
      if (playerIndex < 0 || playerIndex >= LivesHandicap.Length)
        return 0;

      return LivesHandicap[playerIndex];
    }

    public static void AdjustImmunityHandicap(int delta)
    {
      ImmunityHandicap = Calc.Clamp(ImmunityHandicap + delta, 0, MaxImmunityHandicap);
    }

    public static void AdjustLives(int playerIndex, int delta)
    {
      if (playerIndex < 0 || playerIndex >= LivesHandicap.Length)
        return;

      LivesHandicap[playerIndex] = Calc.Clamp(LivesHandicap[playerIndex] + delta, 0, MaxLivesHandicap);
    }

    public static bool HasAnyHandicap()
    {
      for (int i = 0; i < LivesHandicap.Length; i++)
      {
        if (LivesHandicap[i] > 0)
          return true;
      }

      return false;
    }

    public static int GetStartingLives(int playerIndex)
    {
      return GetLivesHandicap(playerIndex);
    }

    //public static void ApplyVictoryHandicap(global::TowerFall.Session session)
    //{
    //  if (session == null)
    //    return;

    //  for (int scoreIndex = 0; scoreIndex < session.Scores.Length; scoreIndex++)
    //  {
    //    int bonus = 0;
    //    for (int playerIndex = 0; playerIndex < TFGame.Players.Length; playerIndex++) //todo 8
    //    {
    //      if (!TFGame.Players[playerIndex])
    //        continue;

    //      if (session.GetScoreIndex(playerIndex) == scoreIndex)
    //        bonus += GetVictoryHandicap(playerIndex);
    //    }

    //    if (bonus <= 0)
    //      continue;

    //    session.Scores[scoreIndex] += bonus;
    //    session.OldScores[scoreIndex] += bonus;
    //  }
    //}
  }
}
