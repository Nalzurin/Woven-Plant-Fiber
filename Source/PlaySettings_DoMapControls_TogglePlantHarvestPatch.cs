using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace PlantFibber
{
    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(PlaySettings))]
    [HarmonyPatch("DoMapControls")]
    public static class PlaySettings_DoMapControls_TogglePlantHarvestPatch
    {
        public static readonly Texture2D Icon = ContentFinder<Texture2D>.Get("UI/Buttons/TogglePlantFiber");
        public static void Postfix(WidgetRow row)
        {
            row.ToggleableIcon(ref Current.Game.GetComponent<GameComponent_PlantFiber>().toggleHarvestPlantFiber, Icon, "PlantFibber.TogglePlantHarvest".Translate());
        }
    }
}
