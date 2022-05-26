using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class TooltipTrigger : MonoBehaviour
{
	[SerializeField] string tooltipText;

	LevelUIManager levelUIManager;
	bool triggered = false;


	private void Start()
	{
		levelUIManager = FindObjectOfType<LevelUIManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !triggered)
		{
			triggered = true;
			levelUIManager.AddTooltipToQueue(tooltipText);
			Destroy(gameObject);
		}
	}
}
