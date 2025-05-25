using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    #region FIELDS

    [SerializeField] 
    private float moveSpeed = 8f;
    [SerializeField] 
    private float rotationSpeed = 12f;
    [SerializeField] 
    private Joystick movementJoystick;
    [SerializeField] 
    private Camera playerCamera;
    [SerializeField] 
    private float cameraSensitivity = 2f;
    [SerializeField] 
    private float cameraDistance = 5f;
    [SerializeField] 
    private Vector2 cameraClamp = new (-30, 60);
    [SerializeField]
    private GameObject traderInteractionButton, openChestButton;
    [SerializeField]
    private AudioSource footstepAudioSource;
    [SerializeField]
    private AudioClip[] footstepAudioClips;

    public event Action InATMStepped, OutATMStepped;
    private GameObject currentATM, currentChest;
    private Vector3 cameraOffset;
    private Vector2 cameraRotation;
    private const string RUNNING_ANIMATION_KEY = "isRunning";


    #endregion

    #region UNITY_METHODS

    private void Start() => cameraOffset = new Vector3(0, 2f, -cameraDistance);

    private void Update()
    {
        Application.targetFrameRate = 999999;
        QualitySettings.vSyncCount = 0;
        HandleMovement();
        HandleCamera();
    }

    #endregion

    #region MAIN_METHODS

    private void HandleMovement()
    {
        Vector3 moveDirection = playerCamera.transform.TransformDirection(
            new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical)
        );
        moveDirection.y = 0;
        if (moveDirection.magnitude > 0.1f)
        {
            transform.position += moveSpeed * Time.deltaTime * moveDirection.normalized;
            GetComponent<Animator>().SetBool(RUNNING_ANIMATION_KEY, true);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        else
            GetComponent<Animator>().SetBool(RUNNING_ANIMATION_KEY, false);
    }

    private void HandleCamera()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    cameraRotation.x += touch.deltaPosition.x * cameraSensitivity * Time.deltaTime;
                    cameraRotation.y = Mathf.Clamp(
                        cameraRotation.y - touch.deltaPosition.y * cameraSensitivity * Time.deltaTime,
                        cameraClamp.x,
                        cameraClamp.y
                    );
                }
            }
        }
        Quaternion rotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);
        playerCamera.transform.position = transform.position + rotation * cameraOffset;
        playerCamera.transform.LookAt(transform.position);
    }

    public void DisableCurrentATM()
    {
        FindFirstObjectByType<PointsSpawnSystem>().ReleasePoint(currentATM.transform);
        currentATM.SetActive(false);
    }

    public void DestroyOpenedChest()
    {
        Destroy(currentChest);
        openChestButton.SetActive(false);
    }

    public void PlayFootstepSound()
    {
        footstepAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        footstepAudioSource.PlayOneShot(footstepAudioClips[UnityEngine.Random.Range(0, footstepAudioClips.Length)]);
    }

    #endregion

    #region ON_TRIGGER_ACTION

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ATM"))
        {
            currentATM = other.gameObject;
            InATMStepped?.Invoke();
        }            
        if (other.gameObject.CompareTag("Chest"))
        {
            currentChest = other.gameObject;
            openChestButton.SetActive(true);
        }
        if (other.gameObject.CompareTag("Trader"))
            traderInteractionButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ATM"))
            OutATMStepped?.Invoke();
        if (other.gameObject.CompareTag("Trader"))
            traderInteractionButton.SetActive(false);
        if (other.gameObject.CompareTag("Chest"))
            openChestButton.SetActive(false);
    }

    #endregion

}
