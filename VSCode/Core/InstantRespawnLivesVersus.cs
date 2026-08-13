#nullable enable
using FortRise;
using Microsoft.Xna.Framework;
using TowerFall;

namespace TFModFortRiseGameModeRespawn
{
  /// <summary>
  /// Versus mode: Players have N lives, respawn immediately when killed if they have remaining lives.
  /// No shield mechanic - normal TowerFall death with instant respawn.
  /// </summary>
  public sealed class Respawn : IVersusGameMode, IRegisterable
  {
    private static ISubtextureEntry RespawnIcon { get; set; } = null!;

    /// <summary>
    /// Entree renvoyee par le registre. Sert notamment a reconnaitre le mode
    /// via <c>MatchSettings.Mode == Respawn.RespawnEntry.Modes</c>, ce qui
    /// remplace le couple IsCustom / CurrentModeName de FortRise 4.
    /// </summary>
    public static IVersusGameModeEntry RespawnEntry { get; private set; } = null!;

    //public string Name => "Instant Respawn Lives";
    public string Name => "Ebe1.Respawn";
    public Color NameColor => new Color(255, 150, 130);
    public ISubtextureEntry Icon => RespawnIcon;
    public bool IsTeamMode => false;

    public static void Register(IModContent content, IModRegistry registry)
    {
      // Icone propre au mode, aux dimensions des quatre du jeu (184x82) et dans leur
      // style : l'archer dans une fleche qui boucle. Elle remplace l'emprunt a
      // "lastManStanding", qui disait le contraire de ce mode-ci.
      RespawnIcon = registry.Subtextures.RegisterTexture(
          content.Root.GetRelativePath("Content/Atlas/gamemode.png")
      );

      RespawnEntry = registry.GameModes.RegisterVersusGameMode(new Respawn());
    }

    public void OnStartGame(Session session)
    {
    }

    public RoundLogic OnCreateRoundLogic(Session session)
    {
      return new RespawnRoundLogic(session);
    }

    public int OverrideCoinOffset(Session? session)
    {
      return 11;
    }
  }
}
