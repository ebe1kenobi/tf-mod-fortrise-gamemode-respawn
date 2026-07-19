// Aucune option exposee pour l'instant : les vies et le delai d'immunite se
// reglent dans la popup (UIVersusHandicapPopup), pas dans le menu FortRise.
// Pour reactiver : decommenter, implementer Create(ISettingsCreate) qui est
// abstraite en FortRise 5, et surcharger CreateSettings() dans le module.

//using FortRise;

//namespace TFModFortRiseGameModeRespawn
//{
//  public class TFModFortRiseGameModeRespawnSettings : ModuleSettings
//  {
//    public override void Create(ISettingsCreate settings)
//    {
//      settings.CreateNumber("Respawn: starting lives (needs > 1)", lifeNumber, (x) => lifeNumber = x, 1, 50);
//      settings.CreateNumber("Respawn: immunity on respawn (seconds)", respawnImmunitySeconds, (x) => respawnImmunitySeconds = x, 0, 10);
//    }

//    public int lifeNumber { get; set; } = 3;
//    public int respawnImmunitySeconds { get; set; } = 2;
//  }
//}
