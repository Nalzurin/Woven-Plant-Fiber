using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace PlantFibber
{
    public class GameComponent_PlantFiber : GameComponent
    {
        public GameComponent_PlantFiber(Game game)
        {
        }

        public bool toggleHarvestPlantFiber = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref toggleHarvestPlantFiber, "toggleHarvestPlantFiber", true);
        }
    }
}
