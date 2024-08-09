using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public GameObject CinemachineCam;

    CinemachineVirtualCamera cam;

    [SerializeField]
    float ZoomSpeed = 5f;

    float SceneReloadDelay;

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the GameObject across scenes
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cam = CinemachineCam.GetComponent<CinemachineVirtualCamera>();
       
    }

    void Update()
    {
        // Example: Zoom in when the player presses a key (e.g., "+")


        /*        if (Input.GetKeyDown(KeyCode.Plus))
                    cam.m_Lens.FieldOfView -= ZoomSpeed * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Minus))
                    cam.m_Lens.FieldOfView += ZoomSpeed * Time.deltaTime;*/
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        cam.m_Lens.FieldOfView += scrollInput * ZoomSpeed;

        //desiredCameraSize += 1f; // Adjust the zoom increment

        // Smoothly transition to the desired zoom level
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredCameraSize, transitionSpeed * Time.deltaTime);

        //float scrollInput = Input.GetAxis("Mouse ScrollWheel");

    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadCurrentSceneWithDelay(float delay)
    {
        SceneReloadDelay = delay;
        StartCoroutine(CoReloadScene());
    }
    private IEnumerator CoReloadScene()
    {
        yield return new WaitForSeconds(SceneReloadDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
