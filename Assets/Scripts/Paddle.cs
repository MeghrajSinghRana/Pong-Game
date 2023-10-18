using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{

    public float speed = 18f;

    private float movementX;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {

        paddleControl();


    }



    private void paddleControl() {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0, 0) * speed * Time.deltaTime;
    }




    public void OnMouseDown()
    {
        SceneManager.LoadScene("Gameplay");
    }



}//class
