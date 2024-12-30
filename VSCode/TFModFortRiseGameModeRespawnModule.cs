using System;
using System.IO;
using System.Reflection;
using System.Xml;
using FortRise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;
using MonoMod.ModInterop;
using MonoMod.Utils;
using TowerFall;

namespace TFModFortRiseGameModeRespawn
{
  [Fort("com.ebe1.kenobi.tfmodfortrisegamemoderespawn", "TFModFortRiseGameModeRespawn")]
  public class TFModFortRiseGameModeRespawnModule : FortModule
  {
    public static TFModFortRiseGameModeRespawnModule Instance;
    public static Atlas RespawnAtlas;
    public static bool EightPlayerMod;


    //public static SpriteData RespawnData;
    public override Type SettingsType => typeof(TFModFortRiseGameModeRespawnSettings);
    public TFModFortRiseGameModeRespawnSettings Settings => (TFModFortRiseGameModeRespawnSettings)Instance.InternalSettings;

    public TFModFortRiseGameModeRespawnModule() 
    {
        Instance = this;
    }

    public override void Initialize()
    {
      EightPlayerMod = IsModExists("WiderSetMod");
    }

    public override void LoadContent()
    {
      RespawnAtlas = Content.LoadAtlas("Atlas/atlas.xml", "Atlas/atlas.png");
      //RespawnData = Content.LoadSpriteData("Atlas/SpriteData/spriteData.xml", RespawnAtlas);
    }

    public override void Load()
    {
      RespawnRoundLogic.Load();
    }

    public override void Unload()
    {
        RespawnRoundLogic.Unload();

    }
  }
}
