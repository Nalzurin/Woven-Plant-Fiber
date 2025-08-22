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
    public class Settings : ModSettings
    {
        public bool toggleHarvestPlantFiber;
        private Dictionary<string, int> stringIntPairs;

        public Dictionary<ThingDef, int> ThingDefIntPairs;

        public bool plantFibberInZone = true;
        public override void ExposeData()
        {
            base.ExposeData();

            if (Scribe.mode == LoadSaveMode.Saving)
            {
                stringIntPairs = new Dictionary<string, int>();
                foreach (var pair in ThingDefIntPairs)
                {
                    stringIntPairs.Add(pair.Key.defName, pair.Value);
                }
            }
            Scribe_Collections.Look(ref stringIntPairs, "stringIntPairs", LookMode.Value, LookMode.Value);
            Scribe_Values.Look(ref plantFibberInZone, "plantFibberInZone");
            Scribe_Values.Look(ref toggleHarvestPlantFiber, "toggleHarvestPlantFiber", true);
        }

        public void FillThingDefIntDictionary()
        {
            ThingDefIntPairs = new Dictionary<ThingDef, int>();
            foreach (ThingDef plantDef in DefDatabase<ThingDef>.AllDefs.Where(x => x.plant != null))
            {
                if (stringIntPairs.TryGetValue(plantDef.defName, out int value))
                {
                    ThingDefIntPairs.Add(plantDef, value);
                }
                else
                {
                    ThingDefIntPairs.Add(plantDef, 0);
                }
            }
        }

        public void FillStringIntDictionary()
        {
            if (stringIntPairs.NullOrEmpty())
            {
                stringIntPairs = new Dictionary<string, int>();
                foreach (ThingDef plantDef in DefDatabase<ThingDef>.AllDefs.Where(x => x.plant != null))
                {
                    float nutrition = plantDef.GetStatValueAbstract(StatDefOf.Nutrition);
                    float plantHarvestYield = plantDef.GetStatValueAbstract(StatDefOf.PlantHarvestYield);
                    int count = (int)(nutrition * 5f + plantHarvestYield * 0.1f);
                    stringIntPairs.Add(plantDef.defName, count);
                }
            }
        }
    }
}
