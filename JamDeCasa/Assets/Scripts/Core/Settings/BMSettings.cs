using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BMSettings/BMSettings")]
public class BMSettings : ScriptableObject
{
	public bool enableConsoleLogs;
	public bool enableFpsDisplay;
	[Range(0f, 1f)]
	public float gameSpeed;
}
