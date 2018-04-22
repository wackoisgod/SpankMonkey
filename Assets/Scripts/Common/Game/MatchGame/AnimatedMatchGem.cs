using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AnimatedMatchGem : MatchGem
{
	private GameObject ownedGem = null;
	private Vector3 oldGemPostion;

	public void Start()
	{
		enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		DestroyImmediate(GetComponent<BoxCollider2D>());
	}

	public void Init(Vector3 newPostion, GameObject owningGem)
	{
		enabled = true;
		gameObject.transform.localPosition = owningGem.transform.localPosition;
		oldGemPostion = newPostion;

		ownedGem = owningGem;

		ownedGem.gameObject.transform.localPosition = newPostion;
		ownedGem.GetComponent<SpriteRenderer>().enabled = false;

		GetComponent<SpriteRenderer>().sprite = owningGem.GetComponent<SpriteRenderer>().sprite;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	public void Move()
	{
		gameObject.transform.DOMove(oldGemPostion, 0.2f).OnComplete(OnFinished);
	}

	public void OnFinished()
	{
		ownedGem.GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<SpriteRenderer>().enabled = false;
		enabled = false;
	}
}

