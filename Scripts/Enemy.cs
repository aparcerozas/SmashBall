using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	//Transform del Player a perseguir
	public Transform goal;
	//Transform del objeto Empty que sirve de punto de inicio
	public Transform spawn;
	//distancia a la que el Enemy detecta al Player
	public float distancia;
	//NavMeshAgent del Enemy
	NavMeshAgent agent;

	//Se inicializa el NavMeshAgent y la distancia
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		distancia = 15;
	}

	void Update() {
		//Si el Player no se acerca al Enemy, este tiene como destino el punto de inicio
		agent.SetDestination(spawn.position);
		//Pero si el Player se acerca, su destino cambia al del Player
		//De esta manera, si el Player se aleja lo suficiente, el Enemy regresa a su punto de inicio
		if (!agent.pathPending && agent.remainingDistance < distancia)
        {
			agent.SetDestination(goal.position);
        }
	}
}
