using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; //target¿∏∑Œ ªÔ¿ª player
    Vector3 offset;      //cam ∫§≈Õø°º≠ player ∫§≈Õ∏¶ ª´ ∞™

    void Start()
    {
        offset = transform.position - player.position; //start«“ ∂ß offset¿ª ¡§«ÿ¡‹ 
        transform.position = player.position + offset;

    }
    void Update()
    {
        transform.position = player.position + offset;
    }
}
