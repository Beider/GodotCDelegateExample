using Godot;
using System;
using System.Collections; 
using System.Collections.Generic; 
 
 /** 
 *      Simple evaluation node that checks a condition
 *      And runs a follow up node depending on result.
 */
public class BehaviorNodeParameterConditional<T> : BehaviorNode { 
    /** Member vars */ 
    public delegate bool EvaluationDelegate(T param); 
    protected BehaviorNode _ifTrue;
    protected BehaviorNode _ifFalse;

    private EvaluationDelegate _eval; 

    private T _param;
 
 
    /** Constructor */ 
    public BehaviorNodeParameterConditional(T parameter, BehaviorNode ifTrue, BehaviorNode ifFalse, EvaluationDelegate eval, String name, bool debugPrint) { 
        _ifTrue = ifTrue;
        _ifFalse = ifFalse;
        _eval = eval;
        _debugPrint = debugPrint;
        _name = name;
        _param = parameter;
    } 
 
    /* Run corresponding action based on evaluation delegate result */ 
    public override NodeStates Evaluate() { 
        if (_eval(_param) == true)
        {
            DebugPrint("Evauluating: '" + _name + "', Result: TRUE");
            return _ifTrue.Evaluate();
        }
        else
        {
            DebugPrint("Evauluating: '" + _name + "', Result: FALSE");
            return _ifFalse.Evaluate();
        }
    } 
}