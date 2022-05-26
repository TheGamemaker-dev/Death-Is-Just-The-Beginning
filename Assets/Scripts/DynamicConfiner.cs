using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Grid))]
public class DynamicConfiner : MonoBehaviour
{
	PolygonCollider2D collider;
	List<Tilemap> tilemaps;

	private void Awake()
	{
#if UNITY_EDITOR
		if (EditorApplication.isPlaying) { return; }

		Debug.Log("Editor is Awake!");

		collider = GetComponent<PolygonCollider2D>();
		tilemaps = GetComponentsInChildren<Tilemap>().ToList();
		Tilemap.tilemapTileChanged += OnTilemapChanged;
#endif
	}

	private void Update()
	{
		if (!EditorApplication.isCompiling) { return; }

		Tilemap.tilemapTileChanged -= OnTilemapChanged;
		Awake();
	}

	void OnTilemapChanged(Tilemap tm, Tilemap.SyncTile[] syncTiles)
	{
		if (!tilemaps.Contains(tm)) return;

		Vector3Int? max = null, min = null;

		foreach (Tilemap tilemap in tilemaps)
		{
			tilemap.CompressBounds();
			BoundsInt bounds = tilemap.cellBounds;
			if (!min.HasValue && !max.HasValue)
			{
				max = bounds.max;
				min = bounds.min;
				continue;
			}
			else
			{
				if(bounds.min.x < min.Value.x || bounds.min.y < min.Value.y)
				{
					min = bounds.min;
				}
				if(bounds.max.x > max.Value.x || bounds.max.y > max.Value.y)
				{
					max = bounds.max;
				}
			}
		}

		Vector3Int newMin = min.Value;
		Vector3Int newMax = max.Value;
		Vector2[] newPoints = new Vector2[4];

		newPoints[0] = new Vector2(newMin.x, newMin.y);
		newPoints[1] = new Vector2(newMax.x, newMin.y);
		newPoints[2] = new Vector2(newMax.x, newMax.y);
		newPoints[3] = new Vector2(newMin.x, newMax.y);

		collider.SetPath(0, newPoints);
	}
}
