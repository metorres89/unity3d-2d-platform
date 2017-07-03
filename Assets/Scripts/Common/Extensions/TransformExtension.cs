using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
	public static GameObject FindIgnoringActiveState(this Transform parent, string name)
	{
		Component[] components = parent.GetComponentsInChildren(typeof(Transform), true);
		foreach(Component c in components){
			if(c.name == name){
				return c.gameObject;
			}
		}
		return null;
	}
}