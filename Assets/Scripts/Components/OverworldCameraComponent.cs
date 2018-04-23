using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class OverworldCameraComponent : MonoBehaviour
{
	private float _scrollDirection;
	private Vector2 _lastMousePosition;

	private bool _needsMove;
	private bool _;

	private void Start()
	{
		
	}

	private void Update()
	{
		_scrollDirection = Input.GetAxis("Mouse Scrollwheel");

		Vector2 newMouse = Input.mousePosition;

	}
}
