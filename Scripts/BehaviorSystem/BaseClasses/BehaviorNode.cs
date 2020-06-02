using Godot;
using System;

/** Base class of behavior tree.
*       Taken from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
*/
[System.Serializable] 
public abstract class BehaviorNode {

	public enum NodeStates
	{
		FAILURE,
		SUCCESS,
		RUNNING
	}
 
	/* The current state of the node */ 
	protected NodeStates m_nodeState; 

	protected bool _debugPrint = false;
	
	protected String _name;
 
	public NodeStates nodeState { 
		get 
		{
			DebugPrint("Completed '" + _name + "', Result: " + m_nodeState.ToString());
			return m_nodeState;
		} 
	} 
 
	/* The constructor for the node */ 
	public BehaviorNode() {} 
 
	/* Implementing classes use this method to evaluate the desired set of conditions */ 
	public abstract NodeStates Evaluate(); 

	/* For debugging */ 
	protected void DebugPrint(String text)
	{
		if (_debugPrint)
		{
			GD.Print(text);
		}
	}
 
}
