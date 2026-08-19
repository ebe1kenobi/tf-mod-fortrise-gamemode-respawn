using System;
using System.Collections.Generic;
using FortRise;
using HarmonyLib;
using Monocle;
using MonoMod.Utils;
using TowerFall;

namespace TFModFortRiseGameModeRespawn
{
  /// <summary>
  /// Repousse la miasma tant que quelqu'un a plus d'une vie.
  ///
  /// **Pourquoi.** La miasma est la reponse du jeu a une manche qui s'eternise : elle
  /// monte et force la decision. Dans un mode ou chacun a cinq vies, elle arrive au
  /// milieu de la partie et la tranche avant que le mode ait joue - on meurt de la
  /// brume avec quatre vies en poche, ce qui n'est pas un depart. Elle n'est donc pas
  /// supprimee, elle est REPORTEE : des que tout le monde est a sa derniere vie, la
  /// manche redevient un versus ordinaire et la miasma reprend son role.
  ///
  /// **Comment.** Deux gestes a chaque image, tant que la condition tient :
  ///
  /// 1. Le compteur de la logique de manche est remis a zero. C'est lui qui declenche
  ///    la sequence ; le laisser courir ferait revenir la brume dans la seconde qui
  ///    suit le passage a la derniere vie, au lieu de repartir d'un compte neuf.
  /// 2. Les entites Miasma deja posees sont retirees. Le compteur seul ne suffit pas :
  ///    une brume nee avant que la condition ne devienne vraie resterait la.
  ///
  /// Le compteur est un champ PRIVE : il se lit par DynamicData, par son nom. Si un
  /// jour il change de nom, le second geste continue de tenir tout seul - la brume
  /// sera retiree a chaque image au lieu de ne jamais naitre. Le pire cas est donc un
  /// clignotement, pas une panne.
  /// </summary>
  public class MiasmaHold : IHookable
  {
    /// <summary>Nom du champ prive qui compte le temps avant la brume.</summary>
    private const string CounterField = "miasmaCounter";

    /// <summary>
    /// Passe a faux au premier echec de lecture du champ, pour ne pas relancer une
    /// reflexion qui echoue soixante fois par seconde.
    /// </summary>
    private static bool counterReachable = true;

    public static void Load(IHarmony harmony)
    {
      harmony.Patch(
          AccessTools.DeclaredMethod(typeof(Level), nameof(Level.Update)),
          postfix: new HarmonyMethod(Update_patch)
      );
    }

    public static void Update_patch(Level __instance)
    {
      try
      {
        Hold(__instance);
      }
      catch (Exception e)
      {
        Logger.Error("MiasmaHold: " + e);
      }
    }

    /// <summary>Remet le champ a chaque nouvelle manche.</summary>
    public static void Reset()
    {
      counterReachable = true;
    }

    private static void Hold(Level level)
    {
      if (level?.Session == null
          || !MyRespawnPlayer.IsRespawnMode(level.Session.MatchSettings))
      {
        return;
      }

      if (Deciding())
      {
        return;
      }

      Delay(level.Session.RoundLogic);
      Dissipate(level);
    }

    /// <summary>
    /// Vrai quand la manche se joue enfin : plus personne n'a de vie de reserve.
    ///
    /// On compte sur les joueurs de la PARTIE et non sur les archers presents : un
    /// joueur en attente de reapparition n'a pas d'entite a l'ecran, et l'oublier
    /// ferait revenir la brume pendant chaque respawn.
    /// </summary>
    private static bool Deciding()
    {
      for (int i = 0; i < TFGame.Players.Length; i++)
      {
        if (!TFGame.Players[i])
        {
          continue;
        }

        if (MyRespawnPlayer.LivesRemaining[i] > 1)
        {
          return false;
        }
      }

      return true;
    }

    private static void Delay(RoundLogic logic)
    {
      if (logic == null || !counterReachable)
      {
        return;
      }

      try
      {
        using var data = DynamicData.For(logic);
        data.Set(CounterField, 0f);
      }
      catch (Exception e)
      {
        counterReachable = false;
        Logger.Info("[Miasma] compteur illisible, on se contente de retirer la brume : "
            + e.Message);
      }
    }

    /// <summary>
    /// Retire les brumes deja posees.
    ///
    /// La liste est recopiee avant d'etre parcourue : retirer une entite pendant
    /// l'enumeration de la couche est le genre de detail qui ne se voit qu'une fois
    /// sur dix parties.
    /// </summary>
    private static void Dissipate(Level level)
    {
      List<Entity> doomed = null;

      foreach (Entity entity in level.Layers[0].Entities)
      {
        if (entity is Miasma)
        {
          doomed ??= new List<Entity>();
          doomed.Add(entity);
        }
      }

      if (doomed == null)
      {
        return;
      }

      foreach (Entity entity in doomed)
      {
        entity.RemoveSelf();
      }
    }
  }
}
