using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Vehicle : MonoBehaviour
{
    //private Vector3 initpos;
    [SerializeField] GameObject vehicleMonkey;
    [SerializeField] GameObject vehicleMonkeyB;
    [SerializeField] GameObject groundMonkey;
    [SerializeField] Image transparent;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera vanCamera;
    Animator anim;

    
    [SerializeField] AudioSource groundMonkeySound;
    [SerializeField] AudioSource vehicleAudioSource;
    [SerializeField] AudioClip [] vehicleSound = new AudioClip[3];
    bool firstBbang = true;

    [SerializeField] ExitManagerRunGame ExitManager;
public IEnumerator TakeMonkey()
    {
        //Vector3 ctrVector = new Vector3(0, 0, 1);
        //yield return new WaitForSeconds(5f);

        /*groundMonkey.GetComponent<Rigidbody>().isKinematic = true;*/

        /*groundMonkey.GetComponent<SphereCollider>().isTrigger = true;*/
        vehicleAudioSource = GetComponent<AudioSource>();
        

        groundMonkey.GetComponent<RunPlayer>().enabled = false;
        groundMonkey.transform.position = Vector3.zero;

        anim = vehicleMonkeyB.GetComponent<Animator>();
        anim.SetInteger("animation", 5);

        anim = groundMonkey.GetComponent<Animator>();
        anim.SetInteger("animation", 1);
        groundMonkeySound.pitch = 0.5f;

        Time.timeScale = 0.4f;

        vehicleAudioSource.PlayOneShot(vehicleSound[2]); //ºÎ¸ª

        

        while (transform.position.z >8f)
        {
            if (transform.position.z < 60f && firstBbang ==true)
            {
                
                firstBbang = false;
                vehicleAudioSource.PlayOneShot(vehicleSound[0]); //»§
            }

            if (transform.position.z <40f)
                transform.position += Vector3.back * Time.unscaledDeltaTime *25;
            else
                transform.position += Vector3.back * Time.unscaledDeltaTime * 40;

            //yield return new WaitForFixedUpdate();
            yield return new WaitForSecondsRealtime(0.01f);
            //yield return new WaitForFixedUpdate();
        }
        groundMonkeySound.gameObject.SetActive(false); //¹ß°ÉÀ½ Á¾·á

        vehicleAudioSource.PlayOneShot(vehicleSound[1],1.0f); //Let's go
        
        Time.timeScale = 0.1f;
        anim.SetInteger("animation", 3);
        yield return new WaitUntil(() => !vehicleAudioSource.isPlaying);

        groundMonkey.SetActive(false);
        transform.Rotate(new Vector3(0, -180, 0));
        vehicleMonkey.SetActive(true);
        anim = vehicleMonkey.GetComponent<Animator>();
        anim.SetInteger("animation", 5);

        //vehicleAudioSource.volume = 0.3f;
        vehicleAudioSource.PlayOneShot(vehicleSound[2]); //ºÎ¸ª
        yield return new WaitForSecondsRealtime(2f);
        
        vanCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        vehicleAudioSource.PlayOneShot(vehicleSound[0]); //»§
        //while (transform.position.z < 155f)

        Color c = transparent.color;
        c.a = 0f;

        while (true)
        {
            transform.position += Vector3.forward * Time.unscaledDeltaTime *30;
            yield return new WaitForSecondsRealtime(0.01f);
            //yield return new WaitForFixedUpdate();
            if (transform.position.z > 50f)
            {
                
                transparent.gameObject.SetActive(true);
                c.a += 0.02f;
                transparent.color = c;
                //yield return new WaitForSecondsRealtime(0.01f);

                if (c.a > 1f)
                {
                    Debug.Log("ExitRunGame");
                    StopAllCoroutines();
                    ExitManager.MonkeyGardenScene();
                    yield break;
                }
            }
        }
        

        
    }
}
