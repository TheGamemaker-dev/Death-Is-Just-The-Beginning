using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    internal bool tooltipOnScreen = false;

    List<string> tooltipQueue = new List<string>();
	Canvas canvas;

	[SerializeField] GameObject tooltipPrefab;

	private void Start()
	{
		canvas = FindObjectOfType<Canvas>();
	}

	private void Update()
	{
		if (!tooltipOnScreen && tooltipQueue.Count != 0)
		{
			ShowNextTooltip();
		}
	}

	public void AddTooltipToQueue(string tooltip)
	{
        tooltipQueue.Add(tooltip);
	}

	void ShowNextTooltip()
	{
		TooltipInstance instance = Instantiate(tooltipPrefab, canvas.transform).GetComponent<TooltipInstance>();
		instance.tooltipText = tooltipQueue[0];
		tooltipQueue.RemoveAt(0);
		tooltipOnScreen = true;
	}

}
