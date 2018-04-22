using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchInput : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public static Vector2 lastPosition = Vector2.zero;
	public GameObject debugSpawn;

	static public Vector2 lastEventPosition
	{
		get
		{
			return lastPosition;
		}
		set { lastPosition = value; }
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!eventData.dragging) return; 

		lastEventPosition = eventData.position;

		var xyz = Camera.allCameras[1];
		RaycastHit2D hit = Physics2D.Raycast(xyz.ScreenToWorldPoint(eventData.position), Vector2.zero);
		if (hit.collider && MatchPuzzle.CurrentGem)
		{
			var gem = hit.collider.GetComponent<MatchGem>();
			if (hit.collider.gameObject != MatchPuzzle.CurrentGem)
			{
				//Debug.Log(hit.collider.name);
				gem.OnDragOver(MatchPuzzle.CurrentGem);
			}

		}
	}

	public void OnMove(AxisEventData eventData)
	{
		throw new NotImplementedException();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		var xyz = Camera.allCameras[1];
		RaycastHit2D hit = Physics2D.Raycast(xyz.ScreenToWorldPoint(eventData.pressPosition), Vector2.zero);
		if (hit.collider)
		{
			var gem = hit.collider.GetComponent<MatchGem>();
			if (gem)
			{
				gem.OnPress(true);
			}

		}

		lastEventPosition = eventData.pressPosition;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (MatchPuzzle.CurrentGem != null)
		{
			MatchPuzzle.CurrentGem.GetComponent<MatchGem>().OnPress(false);
		}

		lastEventPosition = eventData.pressPosition;
	}
}

