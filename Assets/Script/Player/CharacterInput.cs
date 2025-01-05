using System.Data.SqlTypes;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool interact;
    public int switchItem;

    public bool cursorLooked = true;
    public bool cursorInputForLook = true;

    public bool analogMovement = true;

    public float scalePosition = 0.05f;


    void Update()
    {
        OnMove();
        OnJump();
        OnSprint();
        OnLook();
        OnInteract();
        OnSwitchItem();
    }

    public void OnSwitchItem()
    {
        if (Input.anyKey)
        {
            for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha5; i++)
            {
                KeyCode key = (KeyCode)i;
                if (Input.GetKeyDown(key))
                {
                    int exactNumber = i - (int)KeyCode.Alpha0;
                    SwitchItemInput(exactNumber);
                    
                }

            }
        }

    }
    public void OnInteract()
    {
        InteractInput(Input.GetKey(KeyCode.E));
    }

    public void OnMove()
    {
        float horizontalVector = Input.GetAxis("Horizontal");
        float verticalVector = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalVector, verticalVector);
        MoveInput(movement);

    }
    public void OnLook()
    {

        if (cursorInputForLook)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector2 mousePosition = new Vector2(mouseX, -1 * mouseY) * scalePosition;
            LookInput(mousePosition);
        }
    }

    public void OnJump()
    {
        JumpInput(Input.GetButtonDown("Jump"));

    }

    public void OnSprint()
    {
        SprintInput(Input.GetKey(KeyCode.LeftShift));

    }

    /*************************************************/
    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    public void SwitchItemInput(int newItemState)
    {
        switchItem = newItemState;
    }
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void JumpInput(bool newjumpState)
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
