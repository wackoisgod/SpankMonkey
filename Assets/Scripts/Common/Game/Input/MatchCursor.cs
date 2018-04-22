using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteRenderer))]
public class MatchCursor : MonoBehaviour
{
	static public MatchCursor instance;

	Transform internalTransform;
	SpriteRenderer currentSprite;

	SpriteAtlas currentAtlas;
	string currentSpriteName;

	void Awake() { instance = this; }
	void OnDestroy() { instance = null; }

	void Start()
	{
		internalTransform = transform;
		currentSprite = GetComponentInChildren<SpriteRenderer>();
		gameObject.transform.localScale = new Vector2(135, 135);
		currentSprite.sortingOrder = 200;
		if (currentSprite != null)
		{
			currentAtlas = MatchPuzzle.instance.MatchAtlas;
			currentSpriteName = "invalid";
			//	if (currentSprite.d < 200) currentSprite.depth = 200;
		}
	}

	void Update()
	{
		Vector3 pos = MatchInput.lastEventPosition;
		var uiCamera = Camera.allCameras[1];

		if (uiCamera != null)
		{
			// Since the screen can be of different than expected size, we want to convert
			// mouse coordinates to view space, then convert that to world position.
			//	pos.x = Mathf.Clamp01(pos.x / Screen.width);
			//	pos.y = Mathf.Clamp01(pos.y / Screen.height);
			var stupid = uiCamera.ScreenToWorldPoint(pos);
			stupid.z = 200;
			internalTransform.position = stupid;

			// For pixel-perfect results
			if (uiCamera.orthographic)
			{
				Vector3 lp = internalTransform.localPosition;
				lp.x = Mathf.Round(lp.x);
				lp.y = Mathf.Round(lp.y);
				internalTransform.localPosition = lp;
			}
		}
		else
		{
			pos.x -= Screen.width * 0.5f;
			pos.y -= Screen.height * 0.5f;
			pos.x = Mathf.Round(pos.x);
			pos.y = Mathf.Round(pos.y);
			internalTransform.localPosition = pos;
		}
	}

	static public void Clear()
	{
		if (instance != null && instance.currentSprite != null)
			Set(instance.currentAtlas, instance.currentSpriteName);
	}

	static public void Set(SpriteAtlas atlas, string sprite)
	{
		if (instance != null && instance.currentSprite)
		{
			instance.currentSprite.sprite = atlas.GetSprite(sprite);
			instance.Update();
		}
	}
}

