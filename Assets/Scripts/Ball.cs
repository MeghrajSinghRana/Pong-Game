using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private Transform paddleTransform;

    [SerializeField]
    private GameObject powerupObj;

    private GameObject tempGameObject;

    private Rigidbody2D ballBody;

    private SpriteRenderer ballRenderer;

    private Vector3 tempPos, tempVel, powerupPos;

    private bool isReleased;

    private float releaseX, releaseY;

    public static string BLOCK_TAG = "Blocks";

    static string POWERUP_TAG = "Powerup";

    int[] xAxisSpeedRange = {  -12, -11,-8,8,-9,9,-10,10, 11, 12 };



    private void Awake()
    {
        isReleased = false;
        
        ballBody = GetComponent<Rigidbody2D>();

        ballRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        BallRelease();
    }
   




    private void BallRelease()
    { 
        if(!isReleased)
        {
            //stay on paddle
            tempPos = transform.position;
            tempPos.x = paddleTransform.position.x;
            transform.position = tempPos;

            //release 
            if (Input.GetButtonDown("Jump"))
            {
                releaseX = xAxisSpeedRange[Random.Range(0, xAxisSpeedRange.Length)];
                releaseY = Random.Range(8, 12);
                ballBody.AddForce(new Vector2(releaseX, releaseY), ForceMode2D.Impulse);
                                
                isReleased = true;
            }
        }
    }










    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag(BLOCK_TAG))
        {

            int num = Random.Range(1, 10);
            if (num == 1)
            {                
                powerupPos = collision.gameObject.transform.position;
                powerupPos.x += -0.42f;
                StartCoroutine(SpawnPowerup());
            }

            Destroy(collision.gameObject);
            
        }

    }











    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(POWERUP_TAG))
        {
            Destroy(collision.gameObject);
            EnablePowerup();
            StartCoroutine(DisablePowerup());
        }   

        else if (collision.gameObject.CompareTag(BLOCK_TAG))
        {
            Destroy(collision.gameObject);
        }
    }











    public void EnablePowerup()
    {

        GameObject[] blocks = GameObject.FindGameObjectsWithTag(BLOCK_TAG);

        foreach (GameObject block in blocks)
        {
            block.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        ballRenderer.color = Color.red;
        


    }






    IEnumerator DisablePowerup()
    {       
        yield return new WaitForSeconds(5f);

        GameObject[] blocks = GameObject.FindGameObjectsWithTag(BLOCK_TAG);

        foreach (GameObject block in blocks)
        {
            block.GetComponent<BoxCollider2D>().isTrigger = false;          
        }

        ballRenderer.color = Color.white;
    }






    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(powerupObj, powerupPos, Quaternion.identity);
    }



}//class
