using Godot;
using System;
using System.Collections; 
using System.Collections.Generic; 
 
 /** 
 *      Simple action that either returns success or failure
 */
public class BehaviorParameterNodeAction<T> : BehaviorNode { 
	/** Member vars */ 
	public delegate bool ActionDelegate(T param);

	private ActionDelegate _action;

	private T _param;
 
 
	/** Constructor */ 
	public BehaviorParameterNodeAction(ActionDelegate action, T parameter, String name, bool debugPrint) { 
		_action = action;
		_debugPrint = debugPrint;
		_name = name;
		_param = parameter;
	} 
 
	/* Run action */ 
	public override NodeStates Evaluate() { 
		if (_action(_param) == true)
		{
			m_nodeState = NodeStates.SUCCESS;
		}
		else
		{
			m_nodeState = NodeStates.FAILURE;
		}
		return nodeState;
	} 
}
