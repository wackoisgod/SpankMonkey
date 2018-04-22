using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchInput : MonoBehaviour, IEventSystemHandler, IPointerClickHandler, IMoveHandler
{
	public static Vector2 lastPosition = Vector2.zero;

	static public Vector2 lastEventPosition
	{
		get
		{
			return lastPosition;
		}
		set { lastPosition = value; }
	}

	public void OnMove(AxisEventData eventData)
	{
		throw new NotImplementedException();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		// TODO: Save for later ? 
		//Ray clickRay = Camera.main.ScreenPointToRay(eventData.pressPosition);
		//Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 5.5f, 0)); //Should likely be moved and send screen space position instead of world position
		//float distance;
		//groundPlane.Raycast(clickRay, out distance);
		//Vector3 worldPoint = clickRay.GetPoint(distance);

		lastEventPosition = eventData.pressPosition;
	}
}

