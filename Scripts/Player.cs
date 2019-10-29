using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	//Rigidbody del Player
	private Rigidbody rb;
	//bool para comprobar si el Player está tocando el suelo
	public bool isGrounded;
	//int con la velocidad de movimiento del Player
	public int speed = 4;
	//Transform de los objetos Enemy
	public Transform enemy;
	//Animator del Player
	Animator animator;
	//puntuación inicial de la partida
	public int puntos = 0;
	//Text de la puntiación
	public Text countText;

	//En el método de inicio del juego, inicializo el Rigidbody y el Animator del Player
	//Además de inicializar el texto de la puntuación
	void Start () {
		rb = GetComponent<Rigidbody>();
		animator = this.GetComponent<Animator>();
		countText.text = "Points: " + puntos;
	}

	//En el método OnCollisionEnter trato todas las interacciones del Player entrando en Collision de otros objetos
	void OnCollisionEnter(Collision col)
    {
		//En este if compruebo que el Player está tocando el suelo
        if (col.gameObject.tag == "Ground" && isGrounded == false)
        {
            isGrounded = true;
        }
		//En este if compruebo si el Player está tocando la plataforma de salto,
		//y si es correcto, el Player sale disparado hacia arriba
		else if (col.gameObject.tag == "Jump"){
			rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
		}
    }

	//En el método OnTriggerEnter trato todas las interacciones del Player atravesando objetos con la opción OnTrigger activada
	private void OnTriggerEnter(Collider col)
	{
		//Si atraviesa un Enemy, el Player es transportado al punto de inicio
		if (col.gameObject.tag == "Enemy"){
			rb.velocity = new Vector3(0, 0, 0);;
			transform.position = new Vector3(0, 1, 0);
		}
		//Si atraviesa un Ring, este desaparece y aumenta la puntuación
		if (col.gameObject.tag == "Ring"){
			col.gameObject.SetActive(false);
			puntos++;
			setCountText();
		}
	}
	
	//Tanto es Update como FixedUpdate hago comprobaciones cada frame
	void Update () 
	{
		//Con este if, si el Player está tocando el suelo, puedes pulsar Space para saltar
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		//Con este if, el Player puede mantener pulsado Z para aumentar la velocidad de movimiento
		if (Input.GetKeyDown(KeyCode.Z) && isGrounded){
			Debug.Log("Acelerando");
			speed = 12;
			animator.SetBool("BoolSpeed", true);
		}
		//Si se suelta la tecla Z, la velocidad vuelve a su valor inicial
		if (Input.GetKeyUp(KeyCode.Z)){
			Debug.Log("Frenando");
			speed = 4;
			animator.SetBool("BoolSpeed", false);
		}

	}

	void FixedUpdate() 
	{
		//Movimiento del Player con las flechas
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);

		//Aquí comprueba la posición y del Player
		//Si se cae fuera del mapa a una cierta altura, es transportado al punto de inicio
		if (transform.position.y < -20){
			rb.velocity = new Vector3(0, 0, 0);;
			transform.position = new Vector3(0, 1, 0);
		}
	}

	//Método para actualizar la puntuación del Text
	void setCountText()
	{
		countText.text = "Points: " + puntos;
	}


}
