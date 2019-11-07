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
    bool info2;

    public GameObject infoBox;
    public GameObject infoBox2;

    public GameObject infoCircle;
    public GameObject infoCircle2;

    public bool playedSound;
    public bool playedSound1;

    // Start is called before the first frame update
    void Start()
    {
        playedSound = false;


        if (!SceneManager.GetSceneByName("Room2").isLoaded)
        {
            infoBox2 = null;
         
        }

        if (!SceneManager.GetSceneByName("Room2").isLoaded)
        {
            infoCircle2 = null;

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
                    

                    timeToDestroy += Time.deltaTime;

                    if(timeToDestroy >= 2)
                    {
                        infoCircle.SetActive(false);
                        infoBox.SetActive(true);

                        if (SceneManager.GetSceneByName("EntryDoor").isLoaded)
                        {
                            StartCoroutine(PlayEntryVoiceOver("EntryInteractable"));
                        }

                        if (SceneManager.GetSceneByName("Room1").isLoaded)
                        {
                            StartCoroutine(PlayEntryVoiceOver("Room1Interactable"));
                        }

                        if (SceneManager.GetSceneByName("Room2").isLoaded)
                        {
                            StartCoroutine(PlayEntryVoiceOver("Room2Interactable2"));
                        }

                       
                    }
                }

             

                else if(hit.collider.tag == "Info2")
                {

                    timeToDestroy += Time.deltaTime;

                    if (timeToDestroy >= 2)
                    {
                        infoCircle2.SetActive(false);
                        infoBox2.SetActive(true);

                        info2 = true;
                        StartCoroutine(PlayEntryVoiceOver("Room2Interactable1"));
                    }
                }

                else if(hit.collider.tag == "Navigation1")
                {
                    timeToDestroy += Time.deltaTime;
                    if(timeToDestroy >= 2) { RoomTransition.instance.FadeToLevel("Room1"); }
                    
                }

                else if(hit.collider.tag ==  "Navigation2")
                {
                    timeToDestroy += Time.deltaTime;
                    if (timeToDestroy >= 2) { RoomTransition.instance.FadeToLevel("Room2"); }
                    
                }
                else if (hit.collider.tag == "Navigation3")
                {
                    timeToDestroy += Time.deltaTime;
                    if (timeToDestroy >= 2) { RoomTransition.instance.FadeToLevel("WalkWay"); }
                    
                }

                else if(hit.collider.tag == "Navigation4")
                {
                    timeToDestroy += Time.deltaTime;
                    if (timeToDestroy >= 2) { RoomTransition.instance.FadeToLevel("Room3"); }
                    
                }
            }        
        }

        else
        {
            timeToDestroy = 0;
            
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

        if (SceneManager.GetSceneByName("EntryDoor").isLoaded)
        {
            yield return new WaitForSeconds(AudioManager.instance.sounds[1].clip.length);
            playedSound = true;
        }

        else if (SceneManager.GetSceneByName("Room1").isLoaded)
        {
            yield return new WaitForSeconds(AudioManager.instance.sounds[3].clip.length);
            infoCircle.SetActive(true);
            infoBox.SetActive(false);
            playedSound = true;
        }

        else if (SceneManager.GetSceneByName("Room2").isLoaded)
        {
            if (!info2)
            {
                yield return new WaitForSeconds(AudioManager.instance.sounds[9].clip.length);
                infoCircle.SetActive(true);
                infoBox.SetActive(false);
                playedSound = true;
            }

            else
            {
                yield return new WaitForSeconds(AudioManager.instance.sounds[8].clip.length);
                infoCircle2.SetActive(true);
                infoBox2.SetActive(false);
                playedSound1 = true;
                info2 = false;
            }
        }

        
    }
}
