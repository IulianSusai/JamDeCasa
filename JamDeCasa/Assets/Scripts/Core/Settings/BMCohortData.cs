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
	public CohortPlayerInfo player;
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
	public List<StarConditions> starConditions;
}

[Serializable]
public class CohortPlayerInfo
{
	public float moveSpeed;
	public float turnSmoothTime = 0.2f;
	public float speedSmoothTime = 0.1f;
}

[Serializable]
public struct StarConditions
{
	public int maxDies;
	public int stars;
}
