using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCamera : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects = new GameObject[6];
    [SerializeField] GameObject Bananas;
    Animator [] anims = new Animator[6];

    public IEnumerator Clear()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
            anims[i] = gameObjects[i].GetComponent<Animator>();
        }

        while (Bananas.transform.position.y >=0.75f)
        {
            Bananas.transform.position += Vector3.down * 0.7f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            anims[i].SetInteger("animation", 3);
        }
        yield return new WaitForSeconds(1.5f);

    }

}
