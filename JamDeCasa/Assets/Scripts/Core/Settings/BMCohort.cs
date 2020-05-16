using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BMSettings/BMCohort")]
public class BMCohort : ScriptableObject
{
	public List<BMCohortData> cohortList;

	public BMCohortData GetCohort(string _name) {
		foreach(BMCohortData cohort in cohortList) {
			if(cohort.cohortName == _name) {
				return cohort;
			}
		}
		if(cohortList.Count > 0) {
			Debug.LogError("Cohort with name " + _name + " not found!");
			return cohortList[0];
		} else {
			return null;
		}
	}
	
	public BMCohortData GetRandomAvailableCohort() {
		List<BMCohortData> availableCohorts = new List<BMCohortData>();
		foreach(BMCohortData cohort in cohortList) {
			if (cohort.core.isAvailable) {
				availableCohorts.Add(cohort);
			}
		}
		if(availableCohorts.Count > 0) {
			return availableCohorts[Random.Range(0, availableCohorts.Count)];
		} else {
			Debug.LogError("There are no available cohorts");
			return null;
		}
	}

}
