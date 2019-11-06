using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
                        infoCircle.GetComponent<Image>().enabled = false;
                        infoBox.SetActive(true);

                        if (infoBox.activeSelf)
                        {
                            if (SceneManager.GetSceneByName("EntryDoor").isLoaded)
                            {
                                StartCoroutine(PlayEntryVoiceOver("EntryInteractable"));
                            }

                            if (SceneManager.GetSceneByName("Room2").isLoaded)
                            {
                                StartCoroutine(PlayEntryVoiceOver("Room2Interactable1"));
                            }

                        }
                    }
                }

             

                else if(hit.collider.tag == "Info2")
                {
                    GameObject infoCircle = GameObject.FindGameObjectWithTag("Info2");

                    timeToDestroy += Time.deltaTime;

                    if (timeToDestroy >= 1)
                    {
                        infoCircle.GetComponent<Image>().enabled = false;
                        infoBox2.SetActive(true);

                        if (!infoCircle.GetComponent<Image>().enabled)
                        {
                         
                          StartCoroutine(PlayEntryVoiceOver("Room2Interactable2"));
                            
                        }
                    }
                }
            }        
        }

        else
        {
            timeToDestroy = 0;
            //infoBox.SetActive(false);
            //infoBox2.SetActive(false);
        }

        if (playedSound && SceneManager.GetSceneByName("EntryDoor").isLoaded)
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

    public IEnumerator PlayEntryVoiceOver(string clipPlaying)
    {
        AudioManager.instance.PlaySound(clipPlaying);
        yield return new WaitForSeconds(AudioManager.instance.sounds[1].clip.length);
        playedSound = true;
    }
}
