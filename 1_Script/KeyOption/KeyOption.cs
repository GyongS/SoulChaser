using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOption : MonoBehaviour
{
    private static KeyCode _AttackKey = KeyCode.Mouse0;
    public static KeyCode AttackKey { get { return _AttackKey; } }

    private static KeyCode _SlowSkill = KeyCode.E;
    public static KeyCode SlowSkill { get { return _SlowSkill; } }

    private static KeyCode _DashKey = KeyCode.LeftShift;
    public static KeyCode DashKey { get { return _DashKey; } }

    private static KeyCode _UltSkillKey = KeyCode.R;
    public static KeyCode UltSkillKey { get { return _UltSkillKey; } }

    private static KeyCode _ESC = KeyCode.Escape;
    public static KeyCode ESC { get { return _ESC; } }

    private static KeyCode _RushSkillKey = KeyCode.Mouse1;
    public static KeyCode RushSkillKey { get { return _RushSkillKey; } }
        
}
