using System;
using System.Linq;
using Ensage;
using SharpDX;
using Ensage.Common.Menu;
using Ensage.Common;
using System.Collections.Generic;

namespace Tower_Range_2
{
    internal static class TowerRange2
    {
        private static readonly Menu Menu = new Menu("Tower range 2", "Tower Range 2", true);
        private static Hero _me;
        private static readonly ParticleEffect[] Effect1 = new ParticleEffect[30];
        private static readonly ParticleEffect[] Effect2 = new ParticleEffect[30];
        private static List<Building> _building;
        private static List<Unit> _fountain;

        private static int ReloadTime
        {
            get { return Menu.Item("Reload time").GetValue<Slider>().Value; }
        }

        static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("OwnTowers", "My Towers").SetValue(false).SetTooltip("Show your tower range."));
            Menu.AddItem(new MenuItem("EnemyTowers", "Enemies Towers").SetValue(false).SetTooltip("Show the enemies towers range."));
            Menu.AddItem(
                new MenuItem("Reload time", "Reload Time").SetValue(new Slider(500,15000))
                    .SetTooltip("Set how many seconds to reload range check."));
            Menu.AddToMainMenu();
            Game.OnUpdate += FindTower;
            PrintSuccess(string.Format("> Tower Range 2 Loaded!"));
        }

        public static void FindTower(EventArgs args)
        {
            if (!Game.IsInGame)
                return;

            if (Utils.SleepCheck("heroCache"))
            {
                _me = ObjectMgr.LocalHero;

                Utils.Sleep(ReloadTime, "heroCache");
            }

            if (_me == null)
                return;

            if (Utils.SleepCheck("fountainCache"))
            {
                _building = ObjectMgr.GetEntities<Building>()
                    .Where(x => x.IsAlive && (x.ClassID == ClassID.CDOTA_BaseNPC_Tower || x.ClassID == ClassID.CDOTA_BaseNPC_Fort))
                    .ToList();
                _fountain = ObjectMgr.GetEntities<Unit>()
                    .Where(x => x.IsAlive && x.ClassID == ClassID.CDOTA_Unit_Fountain)
                    .ToList();

                Utils.Sleep(ReloadTime, "fountainCache");
            }

            if (!_building.Any() && !_fountain.Any())
                return;
            uint i = 0;

            if (Menu.Item("EnemyTowers").GetValue<bool>() && Utils.SleepCheck("reload1"))
            {
                foreach (Building build in _building.Where(x => x.Team != _me.Team).ToList())
                {
                    if (build != null)
                    {
                        if (build.ClassID == ClassID.CDOTA_BaseNPC_Tower)
                        {
                            i++;
                            if (Effect1[i] == null)
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            if (Effect1[i].GetHighestControlPoint() != 1)
                            {
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                Effect1[i].SetControlPoint(1, new Vector3(850, 0, 0));
                            }
                            if (Effect2[i] == null)
                                Effect2[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            if (Effect2[i].GetHighestControlPoint() != 1)
                            {
                                Effect2[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                Effect2[i].SetControlPoint(1, new Vector3(900, 0, 0));
                            }
                        }
                        if (build.ClassID == ClassID.CDOTA_BaseNPC_Fort)
                        {
                            i++;
                            if (Effect1[i] == null)
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            if (Effect1[i].GetHighestControlPoint() != 1)
                            {
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                Effect1[i].SetControlPoint(1, new Vector3(900, 0, 0));
                            }
                        }
                    }
                }
                foreach (Entity f in _fountain.Where(x => x.Team != _me.Team).ToList())
                {
                    if (f != null)
                    {
                        i++;
                        if (Effect1[i] == null)
                            Effect1[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                        if (Effect1[i].GetHighestControlPoint() != 1)
                        {
                            Effect1[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            Effect1[i].SetControlPoint(1, new Vector3(1200, 0, 0));
                        }
                        if (Effect2[i] == null)
                            Effect2[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                        if (Effect2[i].GetHighestControlPoint() != 1)
                        {
                            Effect2[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            Effect2[i].SetControlPoint(1, new Vector3(1350, 0, 0));
                        }
                    }
                }
                Utils.Sleep(ReloadTime, "reload1");
            }
            if (Menu.Item("OwnTowers").GetValue<bool>() && Utils.SleepCheck("reload"))
            {
                foreach (Building build in _building.Where(x => x.Team == _me.Team).ToList())
                {
                    if (build != null)
                    {
                        if (build.ClassID == ClassID.CDOTA_BaseNPC_Tower)
                        {
                            i++;
                            if (Effect1[i] == null)
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            if (Effect1[i].GetHighestControlPoint() != 1)
                            {
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                Effect1[i].SetControlPoint(1, new Vector3(850, 0, 0));
                            }
                            if (Effect2[i] == null)
                                Effect2[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            if (Effect2[i].GetHighestControlPoint() != 1)
                            {
                                Effect2[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                Effect2[i].SetControlPoint(1, new Vector3(900, 0, 0));
                            }
                        }
                        if (build.ClassID == ClassID.CDOTA_BaseNPC_Fort)
                        {
                            i++;
                            if (Effect1[i] == null)
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            if (Effect1[i].GetHighestControlPoint() != 1)
                            {
                                Effect1[i] = build.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                Effect1[i].SetControlPoint(1, new Vector3(900, 0, 0));
                            }
                        }
                    }
                }
                foreach (Entity f in _fountain.Where(x => x.Team == _me.Team).ToList())
                {
                    if (f != null)
                    {
                        i++;
                        if (Effect1[i] == null)
                            Effect1[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                        if (Effect1[i].GetHighestControlPoint() != 1)
                        {
                            Effect1[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            Effect1[i].SetControlPoint(1, new Vector3(1200, 0, 0));
                        }
                        if (Effect2[i] == null)
                            Effect2[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                        if (Effect2[i].GetHighestControlPoint() != 1)
                        {
                            Effect2[i] = f.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                            Effect2[i].SetControlPoint(1, new Vector3(1350, 0, 0));
                        }
                    }
                }
                Utils.Sleep(ReloadTime, "reload");
            }
        }
        private static void PrintSuccess(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Green, arguments);
        }
        private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
        {
            ConsoleColor clr = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, arguments);
            Console.ForegroundColor = clr;
        }
    }
}
