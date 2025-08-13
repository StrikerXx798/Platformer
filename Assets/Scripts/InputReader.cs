using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private const string JumpButton = "space";
    private const string Horizontal = "Horizontal";

    public event Action<Vector2> MovementInputReceived;
    public event Action JumpActionPerformed;

    private void Update()
    {
        ReadMovementInput();
        ReadJumpActionInput();
    }

    private void ReadMovementInput()
    {
        var movement = new Vector2(Input.GetAxis(Horizontal), 0f);
        MovementInputReceived?.Invoke(movement);
    }

    private void ReadJumpActionInput()
    {
        if (Input.GetKeyDown(JumpButton))
        {
            JumpActionPerformed?.Invoke();
        }
    }
}