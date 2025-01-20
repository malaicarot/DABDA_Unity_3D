using Cinemachine;
using UnityEngine;

public class PlayerBeforeStart : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    [SerializeField] GameObject cameraRoot;
    [SerializeField] Vector3 positionCamera;
    Vector3 positionCameraRoot;
    CharacterInput _input;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        _input = GetComponent<CharacterInput>();

        EnableFeature(false);
        CursorHandle(false);
        setCameraRoot(false);

    }


    public void CursorHandle(bool active)
    {
        _input.SetCursorState(active);
        Cursor.visible = !active;
    }

    public void setCameraRoot(bool isPlaying)
    {
        if (isPlaying)
        {
            Debug.Log(cameraRoot.transform.position -= positionCamera);
            cameraRoot.transform.position -= positionCamera;
        }
        cameraRoot.transform.position += positionCamera;

    }

    public void EnableFeature(bool enableFeature)
    {
        _input.enableMovement = enableFeature;
        characterController.enabled = enableFeature;
        animator.SetBool("Falling", !enableFeature);
    }



}
