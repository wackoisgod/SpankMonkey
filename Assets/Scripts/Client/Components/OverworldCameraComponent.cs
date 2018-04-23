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

	private Camera _camera;

	private enum SelectionTarget
	{
		Unit,
		Nothing,
		City
	}

	private SelectionTarget _target;

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("PointerDown called");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	private Vector2 CalculateMapSpace(Vector3 worldSpace)
	{
		return CalculateMapSpace(new Vector2(worldSpace.x, worldSpace.y));
	}

	private Vector2 CalculateMapSpace(Vector2 worldSpace)
	{
		Vector2 result = worldSpace;
		result *= 100;
		result /= 5;
		return result;
	}

	private Vector2 CalculateScreenToWorldSpace(Vector2 screenSpace)
	{
		Vector2 result = new Vector2(screenSpace.x - Screen.width, screenSpace.y - Screen.height);
		result.Normalize();

		result.y *= _camera.orthographicSize;
		result.x *= (_camera.orthographicSize * (Screen.height / Screen.width));

		return result;
	}
	
	private Vector2 GetMousePositionInMapSpace()
	{
		Vector2 mousePosition = new Vector2(transform.position.x, transform.position.y) + CalculateScreenToWorldSpace(Input.mousePosition);
		return CalculateMapSpace(mousePosition);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_isDragging = true;

		var mouseObjects = OverworldGameSimulation.Instance.SimObjectsUnderPosition(GetMousePositionInMapSpace());
		
		if (mouseObjects.Count > 0)
		{
			
		}
		else
		{
			_target = SelectionTarget.Nothing;
		}

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_isDragging = false;
	}

	private float _dragSpeed = 1f;

	public void OnDrag(PointerEventData eventData)
	{
		//if (!eventData.dragging) return;
		Debug.Log("Dragging");
		transform.Translate(eventData.delta.x * _dragSpeed, eventData.delta.y * _dragSpeed, 0.0f);
	}

	private void Update()
	{
		
	}

}
