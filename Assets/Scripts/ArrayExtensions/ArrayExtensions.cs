using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ArrayExtensions
{

	public static void Fill<T> (this T[] array, T value)
	{
		for (int i = 0; i < array.Length; i++) {
			array [i] = value;
		}
	}

	public static void Fill<T> (this T[] array, T[] valueArray)
	{
		if (array.Length == valueArray.Length) {
			for (int i = 0; i < array.Length; i++) {
				array [i] = valueArray [i];
			}
		}
	}

	public static void Range (this int[] array, int begin, int end, int step)
	{
		int value = begin;
		for (int i = begin; i < end; i++) {
			array [i] = value;
			value += step;
		}
	}

	public static void Print<T> (this T[] array)
	{
		for (int i = 0; i < array.Length; i++) {
			Debug.Log (array [i]);
		}
	}
}
