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
        private const string EffectName = @"particles\ui_mouseactions\drag_selected_ring.vpcf";
        private static readonly Menu Menu = new Menu("Tower range 2", "Tower Range 2", true);
        private static Hero _me;
        private static readonly Dictionary<Unit, ParticleEffect> AttackRangeEffects = new Dictionary<Unit, ParticleEffect>();
        private static readonly Dictionary<Unit, ParticleEffect> TrueSightRangeEffects = new Dictionary<Unit, ParticleEffect>();
        private static List<Unit> _buildings;

        private static int ReloadTime
        {
            get { return Menu.Item("Reload time").GetValue<Slider>().Value; }
        }

        static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("OwnTowers", "My Towers")
                .SetValue(false)
                .SetTooltip("Show your tower range"));
            Menu.AddItem(new MenuItem("EnemyTowers", "Enemies Towers")
                .SetValue(false)
                .SetTooltip("Show the enemies towers range"));
            Menu.AddItem(new MenuItem("Reload time", "Reload Time")
                .SetValue(new Slider(500,15000))
                .SetTooltip("Set how many seconds to reload range check"));
            Menu.AddToMainMenu();

            Game.OnUpdate += Update;
            
            ObjectMgr.OnRemoveEntity += OnRemoveEntity;
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            var key = args.Entity as Unit;
            if (key != null)
            {
                AttackRangeEffects.Remove(key);
                TrueSightRangeEffects.Remove(key);
            }
        }

        private static void Update(EventArgs args)
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
                _buildings = ObjectMgr.GetEntities<Unit>()
                    .Where(x => x.IsAlive)
                    .Where(x => x.ClassID == ClassID.CDOTA_Unit_Fountain || x.ClassID == ClassID.CDOTA_BaseNPC_Tower || x.ClassID == ClassID.CDOTA_BaseNPC_Fort)
                    .ToList();

                Utils.Sleep(ReloadTime, "fountainCache");
            }

            if (!_buildings.Any())
                return;

            var buildings = new List<Unit>();

            if (Menu.Item("EnemyTowers").GetValue<bool>())
            {
                buildings = buildings.Concat(_buildings.Where(x => x.Team != _me.Team)).ToList();
            }

            if (Menu.Item("OwnTowers").GetValue<bool>())
            {
                buildings = buildings.Concat(_buildings.Where(x => x.Team == _me.Team)).ToList();
            }

            if (Utils.SleepCheck("reload1"))
            {
                foreach (var build in buildings)
                {
                    AddEffect(build);
                }
                Utils.Sleep(ReloadTime, "reload1");
            }
        }

        private static void AddEffect(Unit build)
        {
            if (build.ClassID == ClassID.CDOTA_BaseNPC_Tower)
            {
                if (!AttackRangeEffects.ContainsKey(build))
                {
                    AttackRangeEffects[build] = build.AddParticleEffect(EffectName);
                    AttackRangeEffects[build].SetControlPoint(1, new Vector3(255, 255, 255));
                    AttackRangeEffects[build].SetControlPoint(2, new Vector3(850, 255, 0));
                }

                if (!TrueSightRangeEffects.ContainsKey(build))
                {
                    TrueSightRangeEffects[build] = build.AddParticleEffect(EffectName);
                    TrueSightRangeEffects[build].SetControlPoint(1, new Vector3(150, 150, 150));
                    TrueSightRangeEffects[build].SetControlPoint(2, new Vector3(900, 255, 0));
                }
            }
            else if (build.ClassID == ClassID.CDOTA_BaseNPC_Fort)
            {
                if (!AttackRangeEffects.ContainsKey(build))
                {
                    AttackRangeEffects[build] = build.AddParticleEffect(EffectName);
                    AttackRangeEffects[build].SetControlPoint(1, new Vector3(255, 255, 255));
                    AttackRangeEffects[build].SetControlPoint(2, new Vector3(900, 255, 0));
                }
            }
            else
            {
                if (!AttackRangeEffects.ContainsKey(build))
                {
                    AttackRangeEffects[build] = build.AddParticleEffect(EffectName);
                    AttackRangeEffects[build].SetControlPoint(1, new Vector3(255, 255, 255));
                    AttackRangeEffects[build].SetControlPoint(2, new Vector3(1200, 255, 0));
                }

                if (!TrueSightRangeEffects.ContainsKey(build))
                {
                    TrueSightRangeEffects[build] = build.AddParticleEffect(EffectName);
                    TrueSightRangeEffects[build].SetControlPoint(1, new Vector3(150, 150, 150));
                    TrueSightRangeEffects[build].SetControlPoint(2, new Vector3(1350, 255, 0));
                }
            }
        }
    }
}
