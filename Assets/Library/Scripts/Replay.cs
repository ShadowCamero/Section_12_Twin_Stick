﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour {

	public const int bufferFrames = 100;
	private KeyFrame[] keyFrames = new KeyFrame[bufferFrames];

	private Rigidbody rb;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameManager.bIsRecording)
		{
			Record();
		}  else
		{
			PlayBack();
		}
	}

	private void Record()
	{
		rb.isKinematic = false;

		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;

		keyFrames[frame] = new KeyFrame(time, transform.position, transform.rotation);
	}

	private void PlayBack()
	{
		rb.isKinematic = true;
		int frame = Time.frameCount % bufferFrames;

		transform.position = keyFrames[frame].pos;
		transform.rotation = keyFrames[frame].rot;
	}
}


/// <summary>
/// A structure for storing frameTime, location, and rotation
/// </summary>
public struct KeyFrame
{
	public float frameTime;
	public Vector3 pos;
	public Quaternion rot;

	public KeyFrame(float frameTime, Vector3 pos, Quaternion rot)
	{
		this.frameTime = frameTime;
		this.pos = pos;
		this.rot = rot;
	}
}
