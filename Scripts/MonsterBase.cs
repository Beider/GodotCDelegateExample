using Godot;
using System;

/*
	Made by Beider,
	Check my youtube for the accompanying tutorial video:
	https://www.youtube.com/channel/UCM0mBdsjKQ78eGBSSpnQGuQ
*/
public class MonsterBase : KinematicBody2D
{
	protected bool _isMouseOver = false;
	
	[Export]
	public float MaxSpeed = 300f;
	[Export]
	public float Acceleration = 2000f;
	[Export]
	protected NodePath aiBuilderNode;
	
	public float Speed = 0f;
	
	protected Vector2 _motion = Vector2.Zero;
	protected Vector2 _movement_axis = Vector2.Zero;
	
	protected bool _shouldMove = false;
	
	protected BehaviorNode _ai = null;
	
	public RandomNumberGenerator Random;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Random = new RandomNumberGenerator();
		Random.Randomize();
		Speed = MaxSpeed;
		LoadAI();
	}
	
	private void LoadAI()
	{
		Node _aiBuilder = (Node)GetNodeOrNull(aiBuilderNode);
		if (_aiBuilder != null && _aiBuilder is AIBuilder)
		{
			_ai = ((AIBuilder)_aiBuilder).BuildAI(this);
		}
		else
		{
			GD.Print("AI not found for " + Name);
		}
	}
	
	public override void _PhysicsProcess(float delta)
	{
		if (_ai != null)
		{
			_ai.Evaluate();
		}
		
		if (GlobalPosition.DistanceTo(GetGlobalMousePosition()) > 20f && _shouldMove)
		{
			_movement_axis = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		}
		else
		{
			_movement_axis = Vector2.Zero;
		}
		
		if (_movement_axis == Vector2.Zero)
		{
			ApplyFriction(Acceleration * delta);
		}
		else
		{
			ApplyMovement(_movement_axis * Acceleration * delta, Speed);
		}
		_motion = MoveAndSlide(_motion);
	}
	
	protected void ApplyFriction(float amount)
	{
		if (_motion.Length() > amount)
		{
			_motion -= _motion.Normalized() * amount;
		}
		else
		{
			_motion = Vector2.Zero;
		}
	}

	protected virtual void ApplyMovement(Vector2 movement_speed, float max)
	{
		this._motion += movement_speed;
		this._motion = _motion.Clamped(max);
	}
	
	public bool IsMouseOver()
	{
		return _isMouseOver;
	}
	
	public virtual bool Blink()
	{
		return true;
	}
	
	public virtual bool IsBlinkReady()
	{
		return false;
	}
	
	public virtual bool IsBlinking()
	{
		return false;
	}
	
	public bool ActionMove()
	{
		_shouldMove = true;
		return true;
	}
	
	public bool ActionIdle()
	{
		_shouldMove = false;
		return true;
	}
	
	protected int GetUniqueId()
	{
		return Random.RandiRange(0, Int32.MaxValue);
	}
}
