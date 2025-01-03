using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{

    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    public bool cursorLooked = true;
    public bool cursorInputForLook = true;
 
    public bool analogMovement = true;


    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());

        }
    }

    public void OnJump(InputValue value)
    {
        jumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void jumpInput(bool newjumpState)
    {
        jump = newjumpState;
    }
    public void LookInput(Vector2 newLookState)
    {
        look = newLookState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLooked);
    }

    void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
