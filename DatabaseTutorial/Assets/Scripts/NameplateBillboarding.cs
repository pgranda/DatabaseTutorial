﻿using UnityEngine;

public class NameplateBillboarding : MonoBehaviour
{ 
	void Update ()
	{
	    Camera camera = Camera.main;

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

	}
}
