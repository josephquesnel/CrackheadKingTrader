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

    public class CKT_IngestionOutcomeDoer_ReduceHediff : IngestionOutcomeDoer
    {
        public HediffDef hediffDef;

        public float severity = -1f;

        public CKT_IngestionOutcomeDoer_ReduceHediff()
        {
        }

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(this.hediffDef);
            if (hediff != null)
            {
                hediff.Severity += Math.Max(-hediff.Severity, this.severity);
            }            
        }
    }
}
