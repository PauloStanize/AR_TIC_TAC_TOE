using System.Collections;
using System.Collections.Generic;
using TicTacToe_AI_DataMining;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] GameObject tabuleiro = null;
    string[] boardSituation = null;
    AIProcessor aIProcessor = null;
    string player = "x";

    [SerializeField] Material playerX = null;
    [SerializeField] Material playerO = null;

    // Use this for initialization
    void Start() {

        Debug.Log(transform.position);

        FileReader fr = new FileReader();
        List<string[]> knownPlays = fr.ReadFile("C:/Users/Paulo/Documents/teste ia/Assets/Data Mining/tic-tac-toe.data");

        aIProcessor = new AIProcessor(knownPlays);
        boardSituation = new string[] { "b", "b", "b", "b", "b", "b", "b", "b", "b" };

        //playerX = Resources.Load("Images/player_X", typeof(Material)) as Material;
        //playerO = Resources.Load("Images/player_O", typeof(Material)) as Material;
    }

    // Update is called once per frame
    //void Update() {
    //    RaycastHit hit; //Cria um váriavel hit(onde ele bate)
    //    Debug.DrawLine(transform.position, new Vector3(0f, 0f, 5f), Color.red);
    //    if (Physics.Raycast(transform.position, new Vector3(0f, 0f, 5f), out hit, 200.0f))
    //        print("Found an object - distance: " + hit.collider.gameObject.name);
    //}

    string lastTarget = "";
    float timer = 5f;

    private void FixedUpdate()
    {
        RaycastHit hit; //Cria um váriavel hit(onde ele bate)
        //Debug.DrawLine(transform.position, new Vuniector3(0f, 0f, 5f), Color.red);
        Physics.Raycast(transform.position, new Vector3(0f, 0f, 5f), out hit, 200.0f);

        if (Physics.Raycast(transform.position, new Vector3(0f, 0f, 5f), out hit, 200.0f))
        {
            // print("Found an object - distance: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == lastTarget)
            {
                timer -= Time.deltaTime;
                //Debug.Log(timer);
            }
            else
            {
                Debug.Log("reset");
                timer = 5f;
                lastTarget = hit.collider.gameObject.name;
            }

            if (timer <= 3)
            {                
                makePlay(lastTarget);
            }
        }           
    }

    private void makePlay(string position)
    {
        int chosenPosition = int.Parse(position);        

        //se o tabuleiro está cheio ou a posiçao escolhida ja tem uma jogada
        if(aIProcessor.remainingPlays(boardSituation) <= 0 ||
            boardSituation[chosenPosition] != "b") return;

        Debug.Log("Jogada em: " + lastTarget);

        if (aIProcessor.hasWinner(boardSituation))
        {
            callEndGame();
            return;
        }
        else
        {
            //registra a jogada do player
            boardSituation[chosenPosition] = player;
            renderPlay(boardSituation);
        }

        if (aIProcessor.hasWinner(boardSituation))
        {
            callEndGame();
            return;
        }
        else
        {
            //registra a jogada npc
            aIProcessor.playTurn(boardSituation, "hard", "o");
            renderPlay(boardSituation);
            printBoard(boardSituation);

            if (aIProcessor.hasWinner(boardSituation))
            {
                callEndGame();
                return;
            }
        }        
    }

    private void renderPlay(string[] boardSituation)
    {
        string whereToPlay = "";

        for(int i = 0; i < 9; i++)
        {
            if(boardSituation[i] != "b")
            {
                whereToPlay = i.ToString();

                GameObject go = GameObject.Find(whereToPlay);

                Debug.Log(player);

                if (boardSituation[i] == "x")
                {
                    go.GetComponent<MeshRenderer>().sharedMaterial = playerX;
                    Debug.Log("render X");
                    //  go.GetComponent<MeshRenderer>().material = playerX;
                }
                else
                {
                    go.GetComponent<MeshRenderer>().sharedMaterial = playerO;
                    Debug.Log("render O");
                    //   go.GetComponent<MeshRenderer>().material = playerO;
                }
            }            
        } 
    }

    private void printBoard(string[] board)
    {
        string line = board[0] + " | " + board[1] + " | " + board[2] + " | " + "\n" +
                      board[3] + " | " + board[4] + " | " + board[5] + " | " + "\n" +
                      board[6] + " | " + board[7] + " | " + board[8] + " | ";

        Debug.Log(line);
    }

    private void callEndGame()
    {
        Debug.Log("cabou carai");
    }
}
