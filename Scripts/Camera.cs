using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    //El Player al que la Camera sigue
	public GameObject player;
    private Vector3 offset;

    void Start ()
    {
        offset = transform.position - player.transform.position;
    }
    
    //El método LateUpdate se inicializa cada vez que el Player se mueve
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }
}
