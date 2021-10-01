﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI;

namespace CompactBioSculpter
{
    public class WorkGiver_HaulToCompactBiosculpterPod : WorkGiver_HaulToBiosculpterPod
	{

		public override ThingRequest PotentialWorkThingRequest
		{
			get
			{
				return ThingRequest.ForDef(PodDefOf.O21_CompactBiosculpterPod);
			}
		}

		public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			if (!ModLister.CheckIdeology("Biosculpting"))
			{
				return false;
			}
			if (t.IsForbidden(pawn) || !pawn.CanReserve(t, 1, -1, null, forced))
			{
				return false;
			}
			if (pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.Deconstruct) != null)
			{
				return false;
			}
			Comp_CompactBiosculpterPod compBiosculpterPod = t.TryGetComp<Comp_CompactBiosculpterPod>();
			if (compBiosculpterPod == null || !compBiosculpterPod.PowerOn || compBiosculpterPod.State != BiosculpterPodState.LoadingNutrition)
			{
				return false;
			}
			if (t.IsBurning())
			{
				return false;
			}
			if (this.FindNutrition(pawn, compBiosculpterPod).Thing == null)
			{
				JobFailReason.Is("NoFood".Translate(), null);
				return false;
			}
			return true;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Comp_CompactBiosculpterPod compBiosculpterPod = t.TryGetComp<Comp_CompactBiosculpterPod>();
			if (compBiosculpterPod == null)
			{
				return null;
			}
			ThingCount thingCount = this.FindNutrition(pawn, compBiosculpterPod);
			if (thingCount.Thing == null)
			{
				return null;
			}
			Job job = HaulAIUtility.HaulToContainerJob(pawn, thingCount.Thing, t);
			job.count = Mathf.Min(job.count, thingCount.Count);
			return job;
		}
	}
}
