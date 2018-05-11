using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleElements : MonoBehaviour {

	new public string name;
	public GameObject[] items;
	public Animator[] animator;

	public void Toggle () {
		if(animator.Length == 0)
		{
			foreach (GameObject item in items)
				item.SetActive(!item.activeSelf);
			return;
		}
		for (int i = 0; i < items.Length; i++)
		{
			animator[i].SetBool("IsOpen", !animator[i].GetBool("IsOpen"));
		}
	}
}
