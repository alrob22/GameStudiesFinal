using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;



public class AvatarMovement : MonoBehaviour

{
    public int roundPhase;
    public int playerActionType;

    public int enemyHealth;
    public int playerHealth;

    public int playerIndex;
    public int enemyIndex;

    public int playerHeight;
    public int enemyHeight;

    public int[] terrain;

    public int eventProbability;

    // Start is called before the first frame update
    void Start()
    {
        roundPhase = 0;

        playerIndex = 1;
        enemyIndex = 6;

        //to be coded: getter method of specific enemy health
        playerHealth = 100;
        enemyHealth = 30;



        //records height of each pillar
        //to be coded: semi random pillar height generation
            //creates two 
        terrain = new int[8] { 3, 2, 4, 1, 3, 4, 2, 3};


        //records height of pillars avatars occupy
        playerHeight = terrain[playerIndex];
        enemyHeight = terrain[enemyIndex];

    }

    // Update is called once per frame
    void Update()
    {

        //to be coded: Surprise Round


        if (roundPhase == 0)
        {
            //check player action type
            //will be set by button click events

            
            if (playerActionType == 0)
            {
                //melee attack
                if (playerIndex - enemyIndex < 2)
                {
                    enemyHealth -= 5;
                }
                playerActionType = -1;
                roundPhase++;

            } else if (playerActionType == 1)
            {
                //ranged attack
                if (playerIndex - enemyIndex > 1 && playerIndex - enemyIndex < 3)
                {
                    enemyHealth -= 3;
                }
                playerActionType = -1;
                roundPhase++;

            } else if (playerActionType == 2)
            {
                //demolish
                //replace automatic targeting of a pillar with player selection
                if (terrain[playerIndex + 3] > 1)
                {
                    terrain[playerIndex + 3] = terrain[playerIndex + 3] - 1;
                }
                //update targeted pillar sprite (shift downward out of screen)
                playerActionType = -1;
                roundPhase++;
            } else if (playerActionType == 3)
            {
                //move forwards
                if (playerIndex < 3)
                {
                    playerIndex++;
                    playerHeight = terrain[playerIndex];
                }
                playerActionType = -1;
                roundPhase++;

            } else if (playerActionType == 4)
            {
                //move backwards
                if (playerIndex > 0)
                {
                    playerIndex--;
                    playerHeight = terrain[playerIndex];
                }
                playerActionType = -1;
                roundPhase++;
            }

            
        }
        if (roundPhase == 1)
        {
            //enemy phase
            //check if within range
            Random rnd = new Random();

            roundPhase++;
        }

        if (roundPhase == 2)
        {
            eventProbability++;
            //to be coded
        }

    }
}
