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
        typeof(MyVersusModeButton),
    ];

    public TFModFortRiseGameModeRespawnModule(IModContent content, IModuleContext context, ILogger logger) : base(content, context, logger)
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      //TFModFortRiseGameModeRespawn.Logger.Init("TFModFortRiseGameModeRespawn");

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
