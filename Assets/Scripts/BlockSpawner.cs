using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] BlocksArray;

    [SerializeField]
    private int layerCount;

    [SerializeField]
    private bool dFlag;

    private int tempCount;

    private new Vector3 tempPos;










    private void Awake()
    {
        tempPos = transform.position;

    }




    // Start is called before the first frame update
    void Start()
    {



    }






    // Update is called once per frame
    void Update()
    {

        defaultPattern(layerCount);

    }





    private void defaultPattern(int layers) {

        while ((tempCount <= layers) && dFlag)
        {
           
            int picker = Random.Range(0, 6);

            //new row assignment
            if (tempPos.x > 7.8f)
            {
                tempPos.y -= 1.2f;
                tempPos.x = -7.65f;
                tempCount++;
            }

            //reset tempCount to 0 and get out
            if (tempCount == layers)
            {
                tempCount = 0;
                dFlag = false;
                break;
            }

            //Instantiate Block
            Instantiate(BlocksArray[picker], tempPos, transform.rotation);

            //distance between each block
            tempPos.x += 1.9f;           
        }

       
        

    }









}//class
