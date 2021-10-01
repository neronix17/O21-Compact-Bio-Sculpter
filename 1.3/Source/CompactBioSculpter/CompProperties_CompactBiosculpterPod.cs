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
    public class CompProperties_CompactBiosculpterPod : CompProperties_BiosculpterPod
	{
		public CompProperties_CompactBiosculpterPod()
		{
			this.compClass = typeof(Comp_CompactBiosculpterPod);
		}
	}
}
