using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

class MatchInputRayCaster : BaseRaycaster
{
	public RectTransform RectValue;

	public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
	{
		// Convert to view space
		Vector2 pos;
		if (eventCamera == null)
			pos = new Vector2(eventData.position.x / Screen.width, eventData.position.y / Screen.height);
		else
			pos = eventCamera.ScreenToViewportPoint(eventData.position);

		if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
			return;

		Ray ray = new Ray();

		if (eventCamera != null)
			ray = eventCamera.ScreenPointToRay(eventData.position);

		if (RectValue != null)
		{
			if (!RectTransformUtility.RectangleContainsScreenPoint(RectValue, eventData.position, eventCamera))
			{
				return;
			}

		}
		else
		{
			return;
		}

		var castResult = new RaycastResult
		{
			gameObject = RectValue.gameObject,
			module = this,
			distance = 0,
			index = resultAppendList.Count
		};
		resultAppendList.Add(castResult);

		// we only want one item :P
		return;
	}

	public override Camera eventCamera
	{
		get
		{
			return null;
		}
	}
}
