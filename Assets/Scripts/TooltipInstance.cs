using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipInstance : MonoBehaviour
{
	public string tooltipText;

	Text text;
	LevelUIManager levelUIManager;

	private void Start()
	{
		text = GetComponent<Text>();
		levelUIManager = FindObjectOfType<LevelUIManager>();
		StartCoroutine(ShowTooltip());
	}

	IEnumerator ShowTooltip()
	{
		text.text = "";
		for(int i = 0; i < tooltipText.Length; i++)
		{
			text.text += tooltipText[i];
			yield return new WaitForSeconds(.1f);
		}
		yield return new WaitForSeconds(2);
		text.text = "";
		levelUIManager.tooltipOnScreen = false;
		Destroy(gameObject);
	}
}
