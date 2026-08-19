// add customname support for popup
using System;
using System.Diagnostics;
using FortRise;
using Microsoft.Extensions.Logging;

//CHAR_A_DIE -> orange aie
//    *         ouille
namespace TFModFortRiseGameModeRespawn
{
  public class TFModFortRiseGameModeRespawnModule : Mod
  {
    public static TFModFortRiseGameModeRespawnModule Instance;

    private static Type[] Registerables = [
        typeof(Respawn),
    ];

    internal Type[] Hookables = [
        typeof(MyRespawnPlayer),
        typeof(MiasmaHold),
        typeof(MyVersusModeButton),
    ];

    public static TFModFortRiseGameModeRespawnSettings Settings =>
        Instance != null ? Instance.GetSettings<TFModFortRiseGameModeRespawnSettings>() : null;

    public override ModuleSettings CreateSettings()
    {
      return new TFModFortRiseGameModeRespawnSettings();
    }

    /// <summary>
    /// Ecrit les reglages sur disque immediatement.
    ///
    /// FortRise ne les sauvegarde qu'en quittant le menu Options du jeu
    /// (MainMenu.DestroyOptions) ou lors d'une sauvegarde de partie. Une valeur
    /// changee depuis une popup du mod restait donc en memoire et etait perdue en
    /// quittant le jeu. SaveSettings est internal cote FortRise, d'ou la reflexion.
    /// </summary>
    public static void SaveSettingsNow()
    {
      if (Instance == null)
        return;

      try
      {
        var method = typeof(Mod).GetMethod("SaveSettings",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (method != null)
          method.Invoke(Instance, null);
      }
      catch (Exception ex)
      {
        TFModFortRiseGameModeRespawn.Logger.Info($"[Settings] sauvegarde immediate impossible : {ex.Message}");
      }
    }

    public TFModFortRiseGameModeRespawnModule(IModContent content, IModuleContext context, ILogger logger) : base(content, context, logger)
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      TFModFortRiseGameModeRespawn.Logger.Init(logger);

      foreach (var registerable in Registerables)
      {
        registerable.GetMethod(nameof(IRegisterable.Register))!.Invoke(null, [content, context.Registry]);
      }

      foreach (var hookable in Hookables)
      {
        hookable.GetMethod(nameof(IHookable.Load))!.Invoke(null, [context.Harmony]);
      }
    }
  }
}
