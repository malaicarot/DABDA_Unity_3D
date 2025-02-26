using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBeforeStart : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    [SerializeField] GameObject cameraRoot;
    [SerializeField] Vector3 positionCamera;
    CharacterInput _input;

    float xPos;
    float yPos;
    float zPos;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        _input = GetComponent<CharacterInput>();
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            EnableFeature(false);
            CursorHandle(false);
            setCameraRoot(false);
        }
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
    // public void StartGame()
    // {
    //     EnableFeature(true);
    //     CursorHandle(true);
    //     int index = SceneManager.GetActiveScene().buildIndex;
    //     if (SaveManager.SingletonSaveData.checkPointData.checkpointDatas[SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1].mapIndex == index)
    //     {
    //         xPos = SaveManager.SingletonSaveData.checkPointData.checkpointDatas[SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1].xPosionTion;
    //         yPos = SaveManager.SingletonSaveData.checkPointData.checkpointDatas[SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1].yPosionTion;
    //         zPos = SaveManager.SingletonSaveData.checkPointData.checkpointDatas[SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1].zPosionTion;
    //         gameObject.transform.position = new Vector3(xPos, yPos, zPos);
    //     }
    // }
}
