using Godot;
using System;
using System.Collections.Generic;

/*
	Made by Beider,
	Check my youtube for the accompanying tutorial video:
	https://www.youtube.com/channel/UCM0mBdsjKQ78eGBSSpnQGuQ
*/
public class DelegateManager
{
	protected static readonly List<Delegate> EMPTY_LIST = new List<Delegate>();

	/**
		Outer dictionary is the delegate group.
		Inner dictionary is the key for that particular delegate
	*/
	protected Dictionary<int, Dictionary<int, Delegate>> _delegateList = new Dictionary<int, Dictionary<int, Delegate>>();

	/**
		Adds a delegate to the given group with the key
	*/
	public void AddDelegate(int group, int key, Delegate function)
	{
		if (!_delegateList.ContainsKey(group))
		{
			_delegateList.Add(group, new Dictionary<int, Delegate>());
		}
		_delegateList[group].Add(key, function);
	}

	/**
		Get a list containing all delegates in a group
	*/
	public List<Delegate> GetDelegates(int group)
	{
		if (_delegateList.ContainsKey(group))
		{
			return new List<Delegate>(_delegateList[group].Values);
		}
		return EMPTY_LIST;
	}

	/**
		Removes the given key from the group.
		Returns true if removed, false if not found.
	*/
	public bool RemoveDelegate(int group, int key)
	{
		if (_delegateList.ContainsKey(group))
		{
			if (_delegateList[group].ContainsKey(key))
			{
				_delegateList[group].Remove(key);
				return true;
			}
		}
		return false;
	}

	/** Applies a single argument delegate group to a value */
	public T ApplySingleArgumentDelegate<T>(T var, int group)
	{
		T val = var;
		foreach (Delegate d in GetDelegates(group))
		{
			val = (T)d.DynamicInvoke(val);
		}
		return val;
	}

}
