using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;

    private float yaw;
    private float pitch;

    public RaycastHit hit;
    Ray ray;
    public float timeToDestroy;

    public GameObject infoBox;
    public GameObject infoBox2;

    public bool playedSound;

    // Start is called before the first frame update
    void Start()
    {
        playedSound = false;


        if (!SceneManager.GetSceneByName("Room2").isLoaded)
        {
            infoBox2 = null;
         
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;

        CameraMovement();
        hit = new RaycastHit();
        Debug.DrawRay(camera.position, camera.rotation * Vector3.forward * 10000.0f, Color.red);

        ray = new Ray(camera.position, camera.rotation * Vector3.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if(hit.collider.tag == "Info")
                {
                    GameObject infoCircle = GameObject.FindGameObjectWithTag("Info");

                    timeToDestroy += Time.deltaTime;

                    if(timeToDestroy >= 1)
                    {
                        infoCircle.SetActive(false);
                        infoBox.SetActive(true);

                        if (infoBox.activeSelf)
                        {
                            StartCoroutine(PlayEntryVoiceOver());
                   
                        }
                    }
                }
            }        
        }

        else
        {
            timeToDestroy = 0;
        }

        if (playedSound)
        {
            RoomTransition.instance.FadeToLevel("Room1");
        }
    }

    void CameraMovement()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public IEnumerator PlayEntryVoiceOver()
    {
        AudioManager.instance.PlaySound("EntryInteractable");
        yield return new WaitForSeconds(AudioManager.instance.sounds[1].clip.length);
        playedSound = true;
    }
}
