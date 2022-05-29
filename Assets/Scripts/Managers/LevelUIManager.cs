using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    internal bool tooltipOnScreen = false;

    List<string> tooltipQueue = new List<string>();
    Canvas canvas;
    GameObject resetText;
    Player player;

    [SerializeField]
    GameObject tooltipPrefab;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        resetText = GameObject.Find("Reset UI");
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!tooltipOnScreen && tooltipQueue.Count != 0)
        {
            ShowNextTooltip();
        }
        resetText.SetActive(player.holdingRestart);
        if (player.holdingRestart)
        {
            resetText.GetComponentInChildren<Image>().fillAmount =
                player.restartHoldTime / player.restartTime;
        }
    }

    public void AddTooltipToQueue(string tooltip)
    {
        tooltipQueue.Add(tooltip);
    }

    void ShowNextTooltip()
    {
        TooltipInstance instance = Instantiate(tooltipPrefab, canvas.transform)
            .GetComponent<TooltipInstance>();
        instance.tooltipText = tooltipQueue[0];
        tooltipQueue.RemoveAt(0);
        tooltipOnScreen = true;
    }
}
