using System;
using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class PlayerMovement : MonoBehaviour
{
    //Player
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float speedChangeRate = 10f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float gravity = -15.0f;
    [SerializeField] float rotationSmoothTime = 0.12f;


    //Grounded
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundedOffset = -0.14f;
    [SerializeField] float groundedRadius = 0.5f;
    [SerializeField] float fallTimeOut = 0.15f;
    [SerializeField] float jumpTimeOut = 0.1f;


    //Cinemachine
    [SerializeField] GameObject CinemachineTarget;
    [SerializeField] float TopClamp = 70f;
    [SerializeField] float BotClamp = -30f;
    [SerializeField] float CameraAngleOverride = 0.0f;

    [SerializeField] GameObject PlayerFollowCamera;
    [SerializeField] float FOVEnd;
    [SerializeField] float FOVDuration;
    CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] float deltaTime = 5;

    [SerializeField] AudioClip LandingAudio;
    [SerializeField] AudioClip[] FootStepAudio;
    [Range(0, 1)][SerializeField] float FootstepAudioVolume = 0.5f;

    LoadScene loadScene;


    bool isGrounded = true;
    Animator _animator;
    CharacterInput _input;
    CharacterController _controller;
    GameObject _mainCamera;

    //cinemachine
    float _cinemachineTargetPitch;
    float _cinemachineTargetYaw;
    private const float threshold = 0.01f;

    //Player
    float _speed;
    float _rotationVelocity;
    float _verticalVelocity;
    float _terminalVelocity = 53.0f;
    float _animationBlend;
    float _targetRotation = 0;
    public bool isLimitJump = false;

    // Time Out delta 
    float _fallTimeoutDelta;
    float _jumpTimeoutDelta;

    // Animation
    int animIDSpeed;
    int animIDGrounded;
    int animIDJump;
    int animIDFreeFall;
    int animIDMotionSpeed;
    bool _hasAnimator;




    void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

    }
    void Start()
    {
        cinemachineVirtualCamera = PlayerFollowCamera.GetComponent<CinemachineVirtualCamera>();
        _cinemachineTargetYaw = CinemachineTarget.transform.rotation.eulerAngles.y;
        _hasAnimator = TryGetComponent(out _animator);
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<CharacterInput>();
        loadScene = FindFirstObjectByType<LoadScene>();
        _jumpTimeoutDelta = jumpTimeOut;
        _fallTimeoutDelta = fallTimeOut;

        AssignAnimationIDs();

    }
    void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);

        if (_input.enableMovement)
        {
            ProcessMover();
            GroundChecking();
            ProcessJump();
        }

    }

    void LateUpdate()
    {
        CameraRotation();
    }
    void ProcessMover()
    {
        float targetSpeed = _input.sprint ? sprintSpeed : moveSpeed;

        if (_input.move == Vector2.zero)
        {
            targetSpeed = 0;
        }

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
        if (_animationBlend < 0.01f)
        {
            _animationBlend = 0;
        }

        Vector3 inputDirection = new Vector3(_input.move.x, 0, _input.move.y).normalized;

        if (_input.move != Vector2.zero)
        {

            // inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);

        }
        Vector3 targetDirection = Quaternion.Euler(0, _targetRotation, 0) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);

        if (_hasAnimator)
        {
            _animator.SetFloat(animIDSpeed, _animationBlend);
            _animator.SetFloat(animIDMotionSpeed, inputMagnitude);
        }
    }

    void GroundChecking()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundMask, QueryTriggerInteraction.Ignore);

        if (_hasAnimator)
        {
            _animator.SetBool(animIDGrounded, isGrounded);
        }
    }

    void ProcessJump()
    {
        if (isGrounded)
        {
            _fallTimeoutDelta = fallTimeOut;
            if (_hasAnimator)
            {
                _animator.SetBool(animIDJump, false);
                _animator.SetBool(animIDFreeFall, false);
            }
            if (_verticalVelocity < 0)
            {
                _verticalVelocity = -2f;
            }

            if (_input.jump && _jumpTimeoutDelta <= 0)
            {
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
                if (_hasAnimator)
                {
                    _animator.SetBool(animIDJump, true);
                }
            }

            if (_jumpTimeoutDelta >= 0)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }

            if (isLimitJump)
            {
                _verticalVelocity = -Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeOut;
            if (_fallTimeoutDelta >= 0)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                if (_hasAnimator)
                {
                    _animator.SetBool(animIDFreeFall, true);
                }
            }
            _input.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    void CameraRotation()
    {
        if (_input.look.sqrMagnitude >= threshold)
        {
            // float deltaTime = 10;
            _cinemachineTargetYaw += _input.look.x * deltaTime;
            _cinemachineTargetPitch += _input.look.y * deltaTime;
        }
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BotClamp, TopClamp);

        CinemachineTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0);
    }

    float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360)
        {
            lfAngle += 360f;
        }
        if (lfAngle > 360)
        {
            lfAngle -= 360f;
        }

        return Mathf.Clamp(lfAngle, lfMin, lfMax);

    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }


    void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("Speed");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDJump = Animator.StringToHash("Jump");
    }


    void OnFootStep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.8f)
        {
            if (FootStepAudio.Length > 0)
            {
                var index = Random.Range(0, FootStepAudio.Length - 1);
                AudioSource.PlayClipAtPoint(FootStepAudio[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

    }
    void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.8f)
        {
            AudioSource.PlayClipAtPoint(LandingAudio, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        }
    }

    public void Floating()
    {
        _animator.SetBool("Floating", true);
    }

    public void StartCrushing()
    {
        StartCoroutine(Crushing());
    }
    IEnumerator Crushing()
    {
        _animator.SetBool("Crushing", true);
        _animator.SetBool("SpecialAction", true);
        yield return new WaitForSeconds(2.5f);
        _animator.SetBool("Crushing", false);
        _animator.SetBool("SpecialAction", false);
    }

    public void Spray()
    {
        _animator.SetBool("Spray", true);
        _input.enableMovement = false;
        SoundSingleton._instance.EndMusic();
        StartCoroutine(MoveFOV(cinemachineVirtualCamera.m_Lens.FieldOfView, FOVEnd, FOVDuration));
    }

    IEnumerator MoveFOV(float fromFOV, float toFOV, float duration)
    {
        float escape = 0;
        while (escape < duration)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fromFOV, toFOV, escape / duration);
            escape += Time.deltaTime;
            yield return null;
        }
        cinemachineVirtualCamera.m_Lens.FieldOfView = toFOV;
        loadScene.LoadEndScene();
    }

    public void ApplyForce(float force)
    {
        _verticalVelocity = force;
    }


}
