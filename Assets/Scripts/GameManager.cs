using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Este script es el encargado de llevar la logica del juego,
    como mover el fondo de izquierda a derecha, o generar el mapa de 
    manera infinita
*/
public class GameManager : MonoBehaviour
{

    //ref a menus

    public GameObject menuPrincipal;
    public GameObject menuGameOver;



    //Referencia al objeto col 
    public GameObject colprefab;

    //Referencia al las piedras
    public GameObject stone1;
    public GameObject stone2;


    //Referencia a la imagen de fondo del juego
    public Renderer fondo;

    //Variable que guardara las columnas que se generen en el nivel
    public List<GameObject> columns;

    //Variable que guardara las piedras del nivel
    public List<GameObject> obstacles;

    //Velocidad a la que se movera el suelo
    public float velocidad = 2;


    //Variables de gestion del estado de juego
    public bool gameOver = false;
    public bool start = false;



    
    void Start()
    {

        // Crear Mapa
        for(int i = 0; i<21; i++)
        {
            //Crear una nueva columna con la funcion instantiate
            //Junto con las coordenadas donde vamos a colocar ese objeto
            //y la rotacion del objeto que se define con Quaternion
            //Y agregar la columna a la lista de columnas
            columns.Add( Instantiate( colprefab, new Vector2( -10 + i ,-3 ), Quaternion.identity ) );
        }

        //Crear piedras
        obstacles.Add( Instantiate( stone1, new Vector2( 12  , -2 ), Quaternion.identity ));
        obstacles.Add( Instantiate( stone2, new Vector2( 16  , -2 ), Quaternion.identity ));


        
    }

    void Update()
    {
    
        //Esperar el input del usuario para inicar el juego
        if ( start == false )
        {
            if (Input.GetKeyDown( KeyCode.X ))
            {

                start = true;

            }

        }

        //Esperar el input del usuario para reiniciar el juego
        if ( start == true && gameOver == true )
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown( KeyCode.X ))
            {
                //Cargar la escena actual de nuevo
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

        }        

        //Inicar el juego
        if ( start == true && gameOver == false )
        {
            //Quitar el menu principal
            menuPrincipal.SetActive(false);

            //Mover el fondo en la poscicion x cada update
            //Utilizando el Offset del atributo material
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.03f,0) * Time.deltaTime;

            //Mover mapa 
            for(int i = 0; i < columns.Count; i++)
            {

                //Revisar que la posicion en x de la columna no sea menor a -10, en ese caso mover la columna hasta la derecha
                //lo que creara un efecto que parezca que el piso es infinito

                if ( columns[i].transform.position.x <= -10 )
                {
                    //Mover la columna hasta la derecha
                    columns[i].transform.position =  new Vector3(10,-3,0);
                }

            
                //Mueve cada columna hacia la izquierda en una unidad cada update
            columns[i].transform.position = columns[i].transform.position + new Vector3(-1,0,0)  * Time.deltaTime * velocidad;   
            }
            

            //Mover obstaculos 
            for(int i = 0; i < obstacles.Count; i++)
            {

                if ( obstacles[i].transform.position.x <= -10 )
                {
                    float random = Random.Range(11,18);
                    obstacles[i].transform.position = new Vector3(random ,-2 ,0);
                }

            
                //Mueve cada columna piedra la izquierda en una unidad cada update
                obstacles[i].transform.position = obstacles[i].transform.position + new Vector3(-1,0,0)  * Time.deltaTime * velocidad;   
            }
        }

    }
}
