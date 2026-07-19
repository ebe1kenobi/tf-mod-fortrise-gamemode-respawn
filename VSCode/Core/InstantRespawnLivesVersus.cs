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
    public string Name => "Respawn";
    public Color NameColor => new Color(255, 150, 130);
    public ISubtextureEntry Icon => RespawnIcon;
    public bool IsTeamMode => false;

    public static void Register(IModContent content, IModRegistry registry)
    {
      // Le mod n'embarque pas de texture : on reutilise l'icone vanilla.
      // Le callback est resolu paresseusement, une fois les atlas charges.
      RespawnIcon = registry.Subtextures.RegisterTexture(
          "gameModes/respawn",
          () => TFGame.MenuAtlas["gameModes/lastManStanding"],
          SubtextureAtlasDestination.MenuAtlas
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
