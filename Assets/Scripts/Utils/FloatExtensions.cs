using System;
using UnityEngine;
public static class FloatExtensions
{
	public static float RoundToFive(this float value) {

		int roundedValue = Mathf.RoundToInt(value);

		if (value > roundedValue) {
			return roundedValue + 0.5f;
		}
		else {
			return roundedValue - 0.5f;
		}
	}
}