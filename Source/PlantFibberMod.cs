using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace PlantFibber
{
    public class PlantFibberMod : Mod
    {
        private Settings settings;

        public Vector2 scrollPosition = Vector2.zero;
        public PlantFibberMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect scrollLabelRect = new Rect(10, 50, inRect.width - 20, 30);
            Widgets.Label(scrollLabelRect, $"{ThingDefOfLocal.GatheredPlant.LabelCap}");

            Rect scrollRect = new Rect(scrollLabelRect.x, scrollLabelRect.y + scrollLabelRect.height, scrollLabelRect.width, inRect.height - 140);
            Rect scrollVertRect = new Rect(0, 0, scrollRect.x, 30 * settings.ThingDefIntPairs.Count());

            Widgets.BeginScrollView(scrollRect, ref scrollPosition, scrollVertRect);
            for (int i = 0; i < settings.ThingDefIntPairs.Count(); i++)
            {
                ThingDef key = settings.ThingDefIntPairs.ElementAt(i).Key;
                int value = settings.ThingDefIntPairs[key];
                Rect rect = new Rect(scrollRect.x, 30 * i, scrollRect.width - 40, 30);
                value = (int)Widgets.HorizontalSlider(rect, value, 0, 50, false, $"{key.LabelCap}: {value}");
                settings.ThingDefIntPairs[key] = value;
            }
            Widgets.EndScrollView();

            Rect growingZoneRect = new Rect(scrollRect.x, scrollRect.y + scrollRect.height + 20, scrollRect.width, 30);
            Widgets.CheckboxLabeled(growingZoneRect, "PlantFibber.PlantFibberInZone".Translate(), ref settings.plantFibberInZone);
        }
        public override string SettingsCategory()
        {
            return "PlantFibber.PlantFibberMod".Translate();
        }
    }
}
