using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private const int PrimaryActionMouseButton = 0;
    private const string JumpButton = "space";
    private const string Horizontal = "Horizontal";

    public event Action<Vector2> MovementInputReceived;
    public event Action JumpActionPerformed;
    public event Action PrimaryActionPerformed;

    private void Update()
    {
        ReadMovementInput();
        ReadJumpActionInput();
        ReadPrimaryActionInput();
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

    private void ReadPrimaryActionInput()
    {
        if (Input.GetMouseButtonDown(PrimaryActionMouseButton))
        {
            PrimaryActionPerformed?.Invoke();
        }
    }
}