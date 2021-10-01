using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace CompactBioSculpter
{
	[DefOf]
	public static class PodDefOf
	{
		static PodDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(PodDefOf));
		}

		public static ThingDef O21_CompactBiosculpterPod;

	}
}
