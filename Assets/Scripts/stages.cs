using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class stages : MonoBehaviour
{
    [SerializeField]
    GameObject enemy1;
    [SerializeField]
    GameObject enemy2;
    [SerializeField]
    GameObject enemy3;

    [SerializeField]
    GameObject sukrit;

    Vector3 location = new Vector3();

    int stageCount;

    // Start is called before the first frame update
    void Start()
    {
        stageCount = 1;
        location.x = ScreenUtils.ScreenLeft + 0.20f;
        location.z = -Camera.main.transform.position.z;
        location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
        GameObject player = GameObject.Instantiate(sukrit);
        player.transform.position = location;




        location.x = ScreenUtils.ScreenRight - 0.20f;
        location.z = -Camera.main.transform.position.z;


        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(endstage() == true || stageCount>7)
        {
            StartCoroutine(endthis());
        }


        if((nextstage() == true) && stageCount <=7)
        {
            //implement object pooling next time
            switch(stageCount)
            {
                case 1:
                    GameObject num1 = GameObject.Instantiate(enemy1);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num1.transform.position = location;
                    break;
                case 2:
                    GameObject num2 = GameObject.Instantiate(enemy2);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num2.transform.position = location;
                    break;
                case 3:
                    GameObject num3 = GameObject.Instantiate(enemy1);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num3.transform.position = location;
                    GameObject num4 = GameObject.Instantiate(enemy2);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num4.transform.position = location;
                    break;
                case 4:
                    GameObject num5 = GameObject.Instantiate(enemy3);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num5.transform.position = location;
                    break;
                case 5:
                    GameObject num6 = GameObject.Instantiate(enemy1);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num6.transform.position = location;
                    GameObject num7 = GameObject.Instantiate(enemy3);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num7.transform.position = location;
                    break;
                case 6:
                    GameObject num8 = GameObject.Instantiate(enemy2);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num8.transform.position = location;
                    GameObject num9 = GameObject.Instantiate(enemy3);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num9.transform.position = location;
                    break;
                case 7:
                    GameObject num10 = GameObject.Instantiate(enemy1);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num10.transform.position = location;
                    GameObject num612 = GameObject.Instantiate(enemy2);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num612.transform.position = location;
                    GameObject num11 = GameObject.Instantiate(enemy3);
                    location.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                    num11.transform.position = location;
                    break;

                default:
                    break;


            }


            stageCount += 1;
        }
    }

    private IEnumerator endthis()
    {
        Time.timeScale = 0;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1;
        Destroy(GameObject.FindGameObjectWithTag("DifficultyUtils"));
        MenuManager.GoToMenu(MenuName.Main);
    }

    bool endstage()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            return true;
        }
        return false;
    }

    bool nextstage()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            return true;
        }
            return false;
    }
}
