using Godot;
using System;

/**
*   Builds our basic AI which is very simple.
*	Made by Beider,
*	Check my youtube for the accompanying tutorial video:
*	https://www.youtube.com/channel/UCM0mBdsjKQ78eGBSSpnQGuQ
*/
public class BasicAIBuilder : AIBuilder
{
	public override BehaviorNode BuildAI(MonsterBase monster)
	{
		SetMonster(monster);
		if (debugPrint)
		{
			GD.Print("Building AI for " + monster.Name);
		}
		// Actions are in reverse order as we need to construct the tree
		// from the bottom up.
		BehaviorNode blinkOrMove = IsBlinking(ActionIdle(), ActionMove());
		BehaviorNode blinkCheck = IsBlinkReady(ActionBlink(), blinkOrMove);
		return IsMouseOver(blinkCheck, blinkOrMove);
	}
}
