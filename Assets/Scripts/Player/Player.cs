using UnityEngine;

public class Player : MonoBehaviour
{
    private InputReader _input;
    private VampireSkill _vampireSkill;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _vampireSkill = GetComponent<VampireSkill>();
    }

    private void OnEnable()
    {
        _input.VampireSkillActionPerformed += _vampireSkill.ActivateAbility;
    }
    
    private void OnDisable()
    {
        _input.VampireSkillActionPerformed -= _vampireSkill.ActivateAbility;
    }
}