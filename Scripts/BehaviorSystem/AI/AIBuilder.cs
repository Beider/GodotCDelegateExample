using Godot;
using System;

/**
*   Base class for our AI builders so our monsters can easily get their AI.
*	Made by Beider,
*	Check my youtube for the accompanying tutorial video:
*	https://www.youtube.com/channel/UCM0mBdsjKQ78eGBSSpnQGuQ
*/
public class AIBuilder : Node
{
	[Export]
	protected bool debugPrint = false;

	protected MonsterBase _monster = null;

	public virtual BehaviorNode BuildAI(MonsterBase monster)
	{
		SetMonster(monster);
		GD.Print("Should be overridden");
		return null;
	}

	protected void SetMonster(MonsterBase monster)
	{
		_monster = monster;
	}

	protected MonsterBase GetMonster()
	{
		return _monster;
	}

	#region ACTIONS EASY ACCESS

	protected BehaviorNodeAction ActionBlink()
	{
		return new BehaviorNodeAction(GetMonster().Blink, "ActionBlink", debugPrint);
	}
	
	protected BehaviorNodeAction ActionMove()
	{
		return new BehaviorNodeAction(GetMonster().ActionMove, "ActionMove", debugPrint);
	}
	
	protected BehaviorNodeAction ActionIdle()
	{
		return new BehaviorNodeAction(GetMonster().ActionIdle, "ActionIdle", debugPrint);
	}


	#endregion

	#region CONDITIONS EASY ACCESS

	protected BehaviorNodeConditional IsMouseOver(BehaviorNode ifTrue, BehaviorNode ifFalse)
	{
	   return new BehaviorNodeConditional(ifTrue, ifFalse, GetMonster().IsMouseOver, "IsMouseOver", debugPrint);
	}
	
	protected BehaviorNodeConditional IsBlinkReady(BehaviorNode ifTrue, BehaviorNode ifFalse)
	{
	   return new BehaviorNodeConditional(ifTrue, ifFalse, GetMonster().IsBlinkReady, "IsBlinkReady", debugPrint);
	}
	
	protected BehaviorNodeConditional IsBlinking(BehaviorNode ifTrue, BehaviorNode ifFalse)
	{
	   return new BehaviorNodeConditional(ifTrue, ifFalse, GetMonster().IsBlinking, "IsBlinking", debugPrint);
	}

	#endregion
}
