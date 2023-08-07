using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Config
    {
        public Colours Colours = new Colours();
        public Menu Menu = new Menu();
        public Player Player = new Player();
        public Scav Scav = new Scav();
        public Scav ScavPlayer = new Scav();
        public Scav Boss = new Scav();
        public Exfil Exfil = new Exfil();
        public Grenade Grenade = new Grenade();
        public Weapons Weapon = new Weapons();
        public Items Items = new Items();
        public BattleMode BattleMode = new BattleMode();
        public Container Container = new Container();
        public Corpse Corpse = new Corpse();
        public Movement Movement = new Movement();
        public QualityOfLife QualityOfLife = new QualityOfLife();
        public ItemMisc ItemMisc = new ItemMisc();
        public Aimbots Aimbot = new Aimbots();
        public Crosshair Crosshair = new Crosshair();
        public Radar Radar = new Radar();
        public RadarEntity PlayerRadar = new RadarEntity();
        public RadarEntity ScavRadar = new RadarEntity();
        public RadarEntity BossRadar = new RadarEntity();
        public RadarEntity ScavPlayerRadar = new RadarEntity();
        public LocalPlayer LocalPlayer = new LocalPlayer();
        public Atmosphere Atmosphere = new Atmosphere();
        public Hud Hud = new Hud();
    }
}
