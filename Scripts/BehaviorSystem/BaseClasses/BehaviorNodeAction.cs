using Godot;
using System;
using System.Collections; 
using System.Collections.Generic; 
 
 /** 
 *      Simple action that either returns success or failure
 */
public class BehaviorNodeAction : BehaviorNode { 
	/** Member vars */ 
	public delegate bool ActionDelegate();

	private ActionDelegate _action;
 
 
	/** Constructor */ 
	public BehaviorNodeAction(ActionDelegate action, String name, bool debugPrint) { 
		_action = action;
		_debugPrint = debugPrint;
		_name = name;
	} 
 
	/* Run action */ 
	public override NodeStates Evaluate() { 
		if (_action() == true)
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
