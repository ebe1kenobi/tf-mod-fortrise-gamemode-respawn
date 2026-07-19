using Microsoft.Xna.Framework;
using TowerFall;

namespace TFModFortRiseGameModeRespawn
{
  /// <summary>
  /// Custom RoundLogic for Instant Respawn Lives mode
  /// Prevents round end when players have remaining lives
  /// </summary>
  public class RespawnRoundLogic : HeadhuntersRoundLogic
  {
    public RespawnRoundLogic(Session session) : base(session)
    {
    }

    public override void OnPlayerDeath(Player player, PlayerCorpse corpse, int playerIndex, DeathCause deathType, Vector2 position, int killerIndex)
    {
      // Check if this player has remaining lives
      if (MyRespawnPlayer.LivesRemaining[playerIndex] > 0)
      {
        // Don't call base.OnPlayerDeath to prevent round end logic
        // Just handle the death without triggering round end checks
        return;
      }

      // Player is out of lives, use normal death handling
      base.OnPlayerDeath(player, corpse, playerIndex, deathType, position, killerIndex);
    }
  }
}
