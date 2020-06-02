using Godot;
using System;

/*
	Made by Beider,
	Check my youtube for the accompanying tutorial video:
	https://www.youtube.com/channel/UCM0mBdsjKQ78eGBSSpnQGuQ
*/
public class MouseFollower : MonsterBase
{
	[Export]
	protected float BlinkModifier = 1f;
	
	public static readonly float BLINK_COOLDOWN_TIME = 0.6f;
	
	protected AnimationTree _animationTree = null;
	protected AnimationNodeStateMachinePlayback _stateMachine = null;
	
	protected float _blinkCooldown = 0f;
	
	public MouseFollowerDelegateManager DelegateManager = new MouseFollowerDelegateManager();
	
	public override void _Ready()
	{
		base._Ready();
		_animationTree = (AnimationTree)GetNodeOrNull("AnimationTree");
		_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		AddFloatDelegate(MouseFollowerDelegateManager.Group.BLINK_SPEED_MODIFIER, ModifyBlinkSpeed);
	}
	
	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		_blinkCooldown -= delta;
	}
	
	public override bool Blink()
	{
		_stateMachine.Travel("Blink");
		float cooldown = BLINK_COOLDOWN_TIME;
		cooldown = DelegateManager.ApplySingleArgumentDelegate<float>(cooldown, (int)MouseFollowerDelegateManager.Group.BLINK_SPEED_MODIFIER);
		_blinkCooldown = cooldown;
		return true;
	}
	
	public override bool IsBlinkReady()
	{
		return _blinkCooldown <= 0f;
	}
	
	public override bool IsBlinking()
	{
		return _blinkCooldown >= 0f;
	}
	
	private void _on_MouseFollower_mouse_entered()
	{
		_isMouseOver = true;
	}
	
	private void _on_MouseFollower_mouse_exited()
	{
		_isMouseOver = false;
	}
	
	public float ModifyBlinkSpeed(float value)
	{
		return value * BlinkModifier;
	}
	
#region DELEGATES
	
	public int AddFloatDelegate(MouseFollowerDelegateManager.Group group, Func<float, float> function)
	{
		return AddDelegate(group, function);
	}

	public int AddBoolDelegate(MouseFollowerDelegateManager.Group group, Func<bool, bool> function)
	{
		return AddDelegate(group, function);
	}

	public int AddVector2Delegate(MouseFollowerDelegateManager.Group group, Func<Vector2, Vector2> function)
	{
		return AddDelegate(group, function);
	}

	private int AddDelegate(MouseFollowerDelegateManager.Group group, Delegate function)
	{
		int id = GetUniqueId();
		DelegateManager.AddDelegate((int)group, id, function);
		return id;
	}

	/** Removes a delegate by group and key */
	public virtual bool RemoveModifierDelegate(MouseFollowerDelegateManager.Group group, int key)
	{
		return DelegateManager.RemoveDelegate((int)group, key);
	}
	
#endregion
}
