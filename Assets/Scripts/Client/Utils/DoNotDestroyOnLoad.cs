using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
	// ReSharper disable once UnusedMember.Local
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}

