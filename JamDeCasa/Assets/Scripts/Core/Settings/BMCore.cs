using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BMCore
{
	private static BMSettings settings;
	private static BMLevels levels;
	private static BMCohort cohorts;

	public static BMSettings Settings {
		get {
			if(settings == null) {
				settings = Resources.Load<BMSettings>("Settings/BMSettings");
			}
			return settings;
		}
	}

	public static BMLevels Levels {
		get {
			if(levels == null) {
				levels = Resources.Load<BMLevels>("Settings/BMLevels");
			}
			return levels;
		}
	}

	public static BMCohort Cohorts {
		get {
			if(cohorts == null) {
				cohorts = Resources.Load<BMCohort>("Settings/BMCohort");
			}
			return cohorts;
		}
	}

}
