using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace PlantFibber
{
    [HarmonyPatch(typeof(Plant))]
    [HarmonyPatch("PlantCollected")]
    public class Plant_PlantCollected_PlantFibberPatch
    {
        public static bool Prefix(Pawn by, PlantDestructionMode plantDestructionMode, ref Plant __instance)
        {
            if (by.Faction != Find.FactionManager.OfPlayer)
            {
                return true;
            }
            if(!Utility.Settings.toggleHarvestPlantFiber)
            {
                return true;
            }
            Zone zone = by.Map.zoneManager.ZoneAt(__instance.Position);
            if (zone is Zone_Growing && !Utility.Settings.plantFibberInZone)
            {
                return true;
            }
            if(Utility.PlantInsideGrower(__instance))
            {
                return true;
            }
            int count = Utility.GetGatheredPlantCount(__instance.def);
            if (count > 0)
            {
                Thing thing = ThingMaker.MakeThing(ThingDefOfLocal.GatheredPlant);
                thing.stackCount = count;
                GenSpawn.Spawn(thing, by.Position, by.Map);
            }

            return true;
        }
    }
}
