using FortRise;

namespace TFModFortRiseGameModeRespawn
{
  /// <summary>
  /// Reglages du module, doublons volontaires de ceux de la popup (Y sur le bouton
  /// de mode) : les deux ecrans agissent sur les memes valeurs.
  ///
  /// Le nombre de vies est per-joueur cote popup, mais global ici. Changer le
  /// reglage l'applique donc a TOUS les joueurs ; a l'inverse, la popup ne le
  /// remonte ici que si la valeur est la meme pour tout le monde (voir
  /// PlayerHandicap.SyncSettings).
  /// </summary>
  public class TFModFortRiseGameModeRespawnSettings : ModuleSettings
  {

    // FortRise n'ecrit les reglages qu'en SORTANT du menu Options
    // (MainMenu.DestroyOptions) : quitter le jeu depuis ce menu perdait la
    // modification. Chaque changement declenche donc une sauvegarde immediate.
    public override void Create(ISettingsCreate settings)
    {
      settings.CreateNumber("starting lives (needs > 1)", lifeNumber,
          (x) =>
          {
            lifeNumber = x;
            PlayerHandicap.SetLivesForAll(x);
            TFModFortRiseGameModeRespawnModule.SaveSettingsNow();
          },
          1, PlayerHandicap.MaxLivesHandicap);

      settings.CreateNumber("immunity on respawn (seconds)", respawnImmunitySeconds,
          (x) =>
          {
            respawnImmunitySeconds = x;
            PlayerHandicap.SetImmunity(x);
            TFModFortRiseGameModeRespawnModule.SaveSettingsNow();
          },
          0, PlayerHandicap.MaxImmunityHandicap);
    }

    public int lifeNumber { get; set; } = 3;
    public int respawnImmunitySeconds { get; set; } = 2;
  }
}
