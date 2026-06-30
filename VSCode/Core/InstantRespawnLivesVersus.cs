using FortRise;
using Microsoft.Xna.Framework;
using TowerFall;

namespace TFModFortRisePoto
{
  /// <summary>
  /// Versus mode: Players have N lives, respawn immediately when killed if they have remaining lives.
  /// No shield mechanic - normal TowerFall death with instant respawn.
  /// </summary>
  public sealed class Respawn : CustomGameMode
  {
    public override void StartGame(Session session)
    {
    }

    public override RoundLogic CreateRoundLogic(Session session)
    {
      return new RespawnRoundLogic(session);
    }

    public override void Initialize()
    {
      //Name = "Instant Respawn Lives";
      Name = "Respawn";

      ModeType = GameModeType.Versus;
      Icon = TFGame.MenuAtlas["gameModes/lastManStanding"];
      NameColor = new Color(255, 150, 130);
      CoinOffset = 11;
    }

    public override void InitializeSounds()
    {
    }
  }
}
