using Godot;
using System;

public class BehaviorNodeParameterActionAndContinue<T> : BehaviorNode { 
	/** Member vars */ 
	public delegate bool ActionDelegate(T param);

	private ActionDelegate _action;

    protected BehaviorNode _followUp;

	private T _param;
 
 
	/** Constructor */ 
	public BehaviorNodeParameterActionAndContinue(ActionDelegate action, T parameter, BehaviorNode followUp, String name, bool debugPrint) { 
		_action = action;
		_debugPrint = debugPrint;
		_name = name;
		_param = parameter;
        _followUp = followUp;
	} 
 
	/* Run action */ 
	public override NodeStates Evaluate() { 
		_action(_param);
		return _followUp.Evaluate();
	} 
}