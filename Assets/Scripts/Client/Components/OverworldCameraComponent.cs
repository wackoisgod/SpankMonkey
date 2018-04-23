using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


public class OverworldCameraComponent : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	private float _scrollDirection = 0f;
	private bool _isDragging;

	private enum SelectionTarget
	{
		Unit,
		Nothing,
		City
	}

	public void OnPointerDown(PointerEventData eventData)
	{

	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_isDragging = true;

// 		var overworldCam = GetComponent<Camera>();
// 		RaycastHit2D hit = Physics2D.Raycast(overworldCam.ScreenToWorldPoint(eventData.position), Vector2.zero);

		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_isDragging = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!eventData.dragging) return;
		
	}

	private void Update()
	{
		
	}

}
