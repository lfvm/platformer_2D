using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script encargado del movimiento del jugador

public class Player : MonoBehaviour
{

    public float force;
    private Rigidbody2D rigidBody2D;

    //Animator encargado de hacer que el jugador se vea como si corriera o saltara
    private Animator animator;


    //Referencia a la clase GameManager
    public GameManager gameManager;


    void Start()
    {
        //Obtener referencia al componente Animator del sprite y al rigid body
        animator = GetComponent<Animator>();
        rigidBody2D =  GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Checar si el usuario pico la tecla espacio
        if (Input.GetKeyDown( KeyCode.Space ))
        {

            //Establecer la variable del animador como verdadera
            //lo que hara que se dispare la animacion del personaje
            animator.SetBool("isJumping",true);

            //Agregar un vector al rigidbody para hacer que el personaje se mueva en la posicion 'y'
            // en pocas palabras hacer que salte

            rigidBody2D.AddForce(new Vector2(0,force));


        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        //Cuando el sprite choque con el suelo, establecer la variable saltando como false,
        //ocasionando que la animacion correr del personaje se dispare de nuevo
        
        if (other.gameObject.tag == "floor")
        {
            animator.SetBool("isJumping",false);   
        }

        //Chechar si el player choca con un obstaculo
        if (other.gameObject.tag == "obstacle")
        {
            //Ir a la clase del game manager y decir que se termino el juego 
            gameManager.gameOver = true;
        }
        
    }
}
