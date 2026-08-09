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
      SetImmunity(ImmunityHandicap + delta);
    }

    /// <summary>Valeur globale : reglage du module et popup ecrivent au meme endroit.</summary>
    public static void SetImmunity(int value)
    {
      ImmunityHandicap = Calc.Clamp(value, 0, MaxImmunityHandicap);

      var settings = TFModFortRiseGameModeRespawnModule.Settings;
      if (settings != null && settings.respawnImmunitySeconds != ImmunityHandicap)
        settings.respawnImmunitySeconds = ImmunityHandicap;
    }

    /// <summary>
    /// Applique le meme nombre de vies a tous les joueurs. C'est ce que fait le
    /// reglage du module, qui n'a pas de notion de joueur.
    /// </summary>
    public static void SetLivesForAll(int value)
    {
      int clamped = Calc.Clamp(value, 1, MaxLivesHandicap);
      for (int i = 0; i < LivesHandicap.Length; i++)
        LivesHandicap[i] = clamped;
    }

    /// <summary>
    /// Remonte la valeur des vies vers le reglage du module, uniquement si tous les
    /// joueurs partagent la meme : un reglage global ne peut pas representer quatre
    /// valeurs differentes, et l'ecraser donnerait une valeur trompeuse.
    /// </summary>
    public static void SyncSettings()
    {
      var settings = TFModFortRiseGameModeRespawnModule.Settings;
      if (settings == null)
        return;

      // Seuls les joueurs ACTIFS comptent : la popup ne liste qu'eux, alors que le
      // tableau a huit entrees. Comparer les huit laissait les joueurs absents a leur
      // valeur par defaut, donc jamais de valeur commune, et le reglage du module
      // n'etait jamais mis a jour.
      int common = -1;
      for (int i = 0; i < LivesHandicap.Length && i < TFGame.Players.Length; i++)
      {
        if (!TFGame.Players[i])
          continue;

        if (common < 0)
          common = LivesHandicap[i];
        else if (LivesHandicap[i] != common)
          return;
      }

      if (common >= 0 && settings.lifeNumber != common)
        settings.lifeNumber = common;
    }

    public static void AdjustLives(int playerIndex, int delta)
    {
      if (playerIndex < 0 || playerIndex >= LivesHandicap.Length)
        return;

      LivesHandicap[playerIndex] = Calc.Clamp(LivesHandicap[playerIndex] + delta, 1, MaxLivesHandicap);
      SyncSettings();
    }

    public static bool HasAnyHandicap()
    {
      for (int i = 0; i < LivesHandicap.Length; i++)
      {
        if (LivesHandicap[i] > 1)
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
