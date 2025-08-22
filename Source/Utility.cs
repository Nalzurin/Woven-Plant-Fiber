using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Noise;

namespace PlantFibber
{
    [StaticConstructorOnStartup]
    public static class Utility
    {
        public static List<ThingDef> PlantDefs => DefDatabase<ThingDef>.AllDefsListForReading.Where(x => x.plant != null).ToList();

        public static Settings Settings => LoadedModManager.GetMod<PlantFibberMod>().GetSettings<Settings>();
        static Utility()
        {
            Settings.FillStringIntDictionary();
            Settings.FillThingDefIntDictionary();
        }
        public static bool PlantInsideGrower(Plant plant)
        {
            List<Thing> list = plant.Map.thingGrid.ThingsListAt(plant.Position);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Building_PlantGrower)
                {
                    return true;
                }
            }
            return false;
        }
        public static int GetGatheredPlantCount(ThingDef plantDef)
        {
            if (plantDef.plant == null)
            {
                return 0;
            }
            if (Settings.ThingDefIntPairs.TryGetValue(plantDef, out int value))
            {
                return value;
            }
            return 0;
        }
    }
}
