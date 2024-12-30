using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortRise;

namespace TFModFortRiseGameModeRespawn
{
  public class TFModFortRiseGameModeRespawnSettings: ModuleSettings
  {
    public const int Instant = 0;
    public const int Delayed = 1;
    [SettingsOptions("Instant", "Delayed")]
    public int RespawnMode;
  }
}
