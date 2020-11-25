using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Dino playerPerlab;
    public Stone[] stonePrefabs;

    public float spawnTime;

    int score;

    bool isGameOver;
    bool isGameBegun;

    Dino dinoClone;

    public int Score { get => score; set => score = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsGameBegun { get => isGameBegun; }

    public override void Awake()
    {
        MakeSingleton(false);

    }
    // Start is called before the first frame update
     public override void  Start()
    {
        if (playerPerlab)
            dinoClone = Instantiate(playerPerlab, Vector3.zero, Quaternion.identity);

        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }




   IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        isGameBegun = true;

        if(stonePrefabs != null && stonePrefabs.Length >0)
        {
            while(! isGameOver)
            {
                int randIndex = Random.Range(0, stonePrefabs.Length);

                if(stonePrefabs[randIndex]!= null)
                {
                    Instantiate(stonePrefabs[randIndex], new Vector3(dinoClone.transform.position.x, Random.Range(6f, 9f), 0f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnTime);
            }
        }

        yield return null;
    }
}
