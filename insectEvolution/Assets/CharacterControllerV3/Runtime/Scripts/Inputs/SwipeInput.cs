using System;
using UnityEngine;


public class SwipeInput : PointerInput
{

	public event Action OnSwipeLeft, OnSwipeRight, OnSwipeUp, OnSwipeDown;

	private          Vector3 currentPosition;
	private          Vector3 previousPosition;
	private          Vector3 deltaChange;
	private readonly float   distance;
	private readonly float   halfScreenHeight = Screen.height / 2f;
	private readonly float   halfScreenWidth  = Screen.width  / 2f;


	public SwipeInput(float distance)
	{
		this.distance   =  distance;
		OnPointerDown   += PointerDown;
		OnPointerUp     += PointerUp;
		OnPointerUpdate += PointerUpdate;
	}

	void PointerDown(Vector3 mousePosition)
	{
		currentPosition  = mousePosition;
		previousPosition = mousePosition;
	}

	void PointerUp(Vector3 mousePosition)
	{
		previousPosition = mousePosition;
		CalculateSwipe();
	}

	void PointerUpdate(Vector3 mousePosition)
	{
		previousPosition = mousePosition;
	}

	void CalculateSwipe()
	{
		if (currentPosition == Vector3.zero || previousPosition == Vector3.zero) return;
		deltaChange   =  (previousPosition - currentPosition);
		deltaChange.y /= halfScreenHeight;
		deltaChange.x /= halfScreenWidth;
		if (deltaChange.magnitude > distance)
		{
			if (Mathf.Abs(deltaChange.y) > Mathf.Abs(deltaChange.x))
			{
				if (deltaChange.y > 0) OnSwipeUp?.Invoke();
				else OnSwipeDown?.Invoke();
			}
			else
			{
				if (deltaChange.x > 0) OnSwipeRight?.Invoke();
				else OnSwipeLeft?.Invoke();
			}
		}
	}

}