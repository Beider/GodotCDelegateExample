using Godot;
using System;
using System.Collections; 
using System.Collections.Generic; 
 
 /** 
 *      Simple evaluation node that checks a condition
 *      And runs a follow up node depending on result.
 */
public class BehaviorNodeConditional : BehaviorNode { 
    /** Member vars */ 
    public delegate bool EvaluationDelegate(); 
    protected BehaviorNode _ifTrue;
    protected BehaviorNode _ifFalse;

    private EvaluationDelegate _eval; 
 
 
    /** Constructor */ 
    public BehaviorNodeConditional(BehaviorNode ifTrue, BehaviorNode ifFalse, EvaluationDelegate eval, String name, bool debugPrint) { 
        _ifTrue = ifTrue;
        _ifFalse = ifFalse;
        _eval = eval;
        _debugPrint = debugPrint;
        _name = name;
    } 
 
    /* Run corresponding action based on evaluation delegate result */ 
    public override NodeStates Evaluate() { 
        if (_eval() == true)
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