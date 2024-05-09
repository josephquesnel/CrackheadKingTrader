using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace CrackheadKingTrader
{
    [DefOf]
    public static class CKT_DefOf
    {
        public static ThingDef CKT_GammaMax;
        public static ThoughtDef CKT_GammaMaxSoothe;

        static CKT_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(CKT_DefOf));
        }
    }

    public class ThoughtWorker_GammaMax_Soothe : ThoughtWorker
    {
        private const float Radius = 20f;

        public ThoughtWorker_GammaMax_Soothe()
        {
        }

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!p.Spawned)
            {
                return false;
            }
            List<Thing> things = p.Map.listerThings.ThingsOfDef(CKT_DefOf.CKT_GammaMax);

            for (int i = 0; i < things.Count; i++)
            {
                CompPowerTrader compPowerTrader = things[i].TryGetComp<CompPowerTrader>();
                if ((compPowerTrader == null || compPowerTrader.PowerOn) && p.Position.InHorDistOf(things[i].Position, 20f))

                {
                    return true;
                }
            }
            return false;
        }
    }
}
