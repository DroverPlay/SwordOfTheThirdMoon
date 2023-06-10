using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class CharacterController : MonoBehaviour
{
    public bool jumper = false;
    public Animator anim;
    public Rigidbody rig;
    public Transform mainCamera;
    private Camera _mainCamera;
    public float jumpForce = 3.5f;
    public float walkingSpeed = 2f;
    public float runningSpeed = 6f;
    public float currentSpeed;
    private float animationInterpolation = 1f;
    public InventoryManager inventoryManager;
    public QuickslotInventory quickslotInventory;
    public Transform AimTarget;
    public static bool _bool;

    [SerializeField] private GameObject crosshair;
    [SerializeField] private CinemachineVirtualCamera CVC;
    [SerializeField] GameObject _escPanel;
    public bool isOpened;
    Camera cashedCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cashedCamera = Camera.main;
        _escPanel.SetActive(false);
    }
    void Run()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);
        anim.SetFloat("x", Input.GetAxis("Horizontal") * animationInterpolation);
        anim.SetFloat("y", Input.GetAxis("Vertical") * animationInterpolation);
        currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, Time.deltaTime * 3);
    }
    void Walk()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1f, Time.deltaTime * 3);
        anim.SetFloat("x", Input.GetAxis("Horizontal") * animationInterpolation);
        anim.SetFloat("y", Input.GetAxis("Vertical") * animationInterpolation);
        currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
    }
    public void ChangeLayerWeight(float newLayerWeight)
    {
        StartCoroutine(SmoothLayerWeightChange(anim.GetLayerWeight(1), newLayerWeight, 0.3f));
    }
    IEnumerator SmoothLayerWeightChange(float oldWeight, float newWeight, float changeDuration)
    {
        float elapsed = 0f;
        while (elapsed < changeDuration)
        {
            float currentWeight = Mathf.Lerp(oldWeight, newWeight, elapsed / changeDuration);
            anim.SetLayerWeight(1, currentWeight);
            elapsed += Time.deltaTime;
            yield return null;
        }
        anim.SetLayerWeight(1, newWeight);
    }
    //Метод возврата в игру для кнопки
    public void ReturnToGame()
    {
        _escPanel.SetActive(false);
        crosshair.SetActive(true);
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isOpened = false;
    }
    private void Update()
    {
        //открытие эскейп меню
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                _escPanel.SetActive(true);
                crosshair.SetActive(false);
                CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
                CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
                CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ContinueData.boolEsc = isOpened;
                Debug.Log("ESC boll - " + ContinueData.boolEsc);
            }
            else
            {
                ContinueData.boolEsc = false;
                Debug.Log("ESC boll - " + ContinueData.boolEsc);
                ReturnToGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (quickslotInventory.activeSlot != null)
            {
                if (quickslotInventory.activeSlot.item != null)
                {
                    if (quickslotInventory.activeSlot.item.itemType == ItemType.Instrument)
                    {
                        if (inventoryManager.isOpened == false)
                        {
                            anim.SetBool("Hit", true);
                        }
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("Hit", false);
        }
        //Анимация подбора предметов
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float reachDistance = 3;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                if (hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    anim.SetTrigger("Take");
                }
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Walk();
            }
            else
            {
                Run();
            }
        }
        else
        {
            Walk();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            jumper = false;
        }
        ///Анимация наклона туловища и головы
        Ray desiredTargetRay = cashedCamera.GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction * 0.7f;
        AimTarget.position = desiredTargetPosition;
    }
    void FixedUpdate()
    {
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        camF.y = 0;
        camR.y = 0;
        Vector3 movingVector;
        movingVector = Vector3.ClampMagnitude(camF.normalized * Input.GetAxis("Vertical") * currentSpeed + camR.normalized * Input.GetAxis("Horizontal") * currentSpeed, currentSpeed);
        anim.SetFloat("magnitude", movingVector.magnitude / currentSpeed);
        rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
        rig.angularVelocity = Vector3.zero;
    }
    public void Jump()
    {
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void Hit()
    {
        foreach (Transform item in quickslotInventory.allWeapons)
        {
            if (item.gameObject.activeSelf)
            {
                item.GetComponent<GetherResources>().CreateResource();
            }
        }
    }
}