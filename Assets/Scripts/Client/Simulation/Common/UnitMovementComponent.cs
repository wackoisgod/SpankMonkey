using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class UnitMovementComponent : MonoBehaviour
{
	private Vector3 _destination;
	private bool _needsMove;
	public Vector3 Destination
	{
		get
		{
			return _destination;
		}
		set
		{
			if (value != _destination)
			{
				_needsMove = true;
				_destination = value;
			}
		}
	}

	private float _minDistance = 0.01f;
	public float Speed = 0.09f;

	private void Update()
	{
		if (_needsMove)
		{
			if (Vector3.Distance(transform.position, _destination) < _minDistance)
			{
				transform.Translate(_destination - transform.position * Speed);
			}
		}
	}

}
