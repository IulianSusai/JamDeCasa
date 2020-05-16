using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BMCohortData
{
	public string cohortName;
	public CohortCore core;
	public CohortGameplay gameplay;
}

[Serializable]
public class CohortCore
{
	public bool isAvailable;
	[BMLevel] public string levelSettingsName;
}

[Serializable]
public class CohortGameplay
{
	public float levelMaxTime;
	public float levelSpeedUpMultiplier;
	public float timeObjectPushForce = 10f;
	public float winPageDelay;
	public float gameOverPageDelay;

	public float shakeDuration = 1f;
	public float dieShakeAmount = 0.7f;
	public float dieDecreaseFactor = 1.0f;

	public float explosionPower;
	public float explosionUpPower;
	public float explosionRadius;
}
