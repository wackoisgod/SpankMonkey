using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


class OverworldInput : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

	public void OnPointerDown(PointerEventData eventData)
	{
		
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!eventData.dragging) return;


	}

}
