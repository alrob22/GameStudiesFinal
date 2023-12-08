using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject col4;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;


    public BattleState state;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Button moveLButton;
    public Button moveRButton;
    public Button attackButton;
    public Button destroyButton;
    public Button healButton;

    int playerPosition;
    int playerElevation;
    int enemyElevation;
    int col4Elevation;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());


    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerUnit.currentHP = PlayerStats.health;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);


        playerPosition = 2;
        playerElevation = 3;
        enemyElevation = 3;
        col4Elevation = 4;


        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator EnemyTurn()
    {
        moveLButton.gameObject.SetActive(false);
        moveRButton.gameObject.SetActive(false);
        healButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);

        dialogueText.text = enemyUnit.unitName + " attacks you with acorns!";

        yield return new WaitForSeconds(2f);

        //roll
        float attackRoll = Random.Range(0, 100);

        //subtract for cover
        if (playerPosition == 0)
        {
            attackRoll = attackRoll - 10;
        }

        if (attackRoll >= 51)
        {
            int damage = enemyUnit.damage;

            //enhance for higher elevation than player
            if (enemyElevation > playerElevation)
            {
                damage = damage + 2 * (enemyElevation - playerElevation);
            }

            bool isDead = playerUnit.TakeDamage(damage);

            playerHUD.SetHP(playerUnit.currentHP);

            dialogueText.text = "The acorns hit you!";


            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        else
        {
            dialogueText.text = "You dodge the acorns!";

            yield return new WaitForSeconds(1f);

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You killed him!";
            PlayerStats.health = playerUnit.currentHP;
            SceneManager.LoadScene(PlayerStats.level);

        } else if (state == BattleState.LOST)
        {
            PlayerStats.health = 25;
            dialogueText.text = "You were defeated.";
            PlayerStats.killed = new string[10];
            PlayerStats.x = 0;
            SceneManager.LoadScene("Start");
        }

    }


    void PlayerTurn()
    {
        dialogueText.text = "CHOOSE AN ACTION";

        if (playerPosition > 0)
        {
            moveLButton.gameObject.SetActive(true);
        }
        if (playerPosition < 3)
        {
            moveRButton.gameObject.SetActive(true);
        }
        if (col4Elevation > 1)
        {
            destroyButton.gameObject.SetActive(true);
        }

        healButton.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(true);
    }

    public void OnHealButton()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        dialogueText.text = "Healing...";

        StartCoroutine(PlayerHeal());

    }

    IEnumerator PlayerHeal()
    {
        yield return new WaitForSeconds(1f);

        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Armor partially repaired.";

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        dialogueText.text = "Attacking...";

        StartCoroutine(PlayerAttack());

    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);

        //roll to see if successful
        float attackRoll = Random.Range(0, 100);

        //subtract cover from attack roll
        if (col4Elevation > enemyElevation)
        {
            attackRoll = attackRoll - (10 * (col4Elevation - enemyElevation));
        }


        if (attackRoll >= 51)
        {

            int damage = playerUnit.damage;

            //enhance damage by elevation (make sure it isn't negative)
            if (playerElevation > enemyElevation)
            {
                damage = damage + 2 * (playerElevation - enemyElevation);
            }

            bool isDead = enemyUnit.TakeDamage(damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The attack is successful.";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }

        }
        else
        {
            dialogueText.text = "The attack missed.";
            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //OnDestroyButton
    public void OnDestroyButton()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        dialogueText.text = "Demolishing Terrain...";

        StartCoroutine(Destroy());

    }

    IEnumerator Destroy()
    {

        col4Elevation = col4Elevation - 1;

        //move column down by -1
        if (col4Elevation == 3)
        {
            col4.transform.position = new Vector3((float)1.41, -4, 0);
        } else if (col4Elevation == 2)
        {
            col4.transform.position = new Vector3((float)1.41, -5, 0);
        } else if (col4Elevation == 1)
        {
            col4.transform.position = new Vector3((float)1.41, -6, 0);
        }



        yield return new WaitForSeconds(1f);



        dialogueText.text = "Demolition Complete.";

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }


    //OnMoveLButton
    public void OnMoveLButton()
    {

        if (state != BattleState.PLAYERTURN || playerPosition == 0)
            return;

        dialogueText.text = "Moving Left...";

        StartCoroutine(MoveLeft());

    }

    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(1f);

        //Move Sprite depending on location
        if (playerPosition == 1)
        {
            playerPrefab.transform.localPosition = new Vector3((float)-4.5, -1, 0);
            playerElevation = 3;
        }
        else if (playerPosition == 2)
        {
            playerPrefab.transform.localPosition = new Vector3((float)-2.2, (float)0.1, 0);
            playerElevation = 4;
        }
        else if (playerPosition == 3)
        {
            playerPrefab.transform.localPosition = new Vector3(0, -1, 0);
            playerElevation = 3;
        }

        dialogueText.text = "Move Complete.";

        yield return new WaitForSeconds(1f);

        playerPosition = playerPosition - 1;

        //update player elevation


        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    //OnMoveRButton
    public void OnMoveRButton()
    {

        if (state != BattleState.PLAYERTURN || playerPosition == 3)
            return;

        dialogueText.text = "Moving Right...";

        StartCoroutine(MoveRight());

    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(1f);

        //Move Sprite depending on location
        if (playerPosition == 0)
        {
            playerPrefab.transform.localPosition = new Vector3((float)-2.2, (float)0.1, 0);
            playerElevation = 4;
        } else if (playerPosition == 1)
        {
            playerPrefab.transform.localPosition = new Vector3(0, -1, 0);
            playerElevation = 3;
        } else if (playerPosition == 2)
        {
            playerPrefab.transform.localPosition = new Vector3(2, -2, 0);
            playerElevation = 2;
        }

        dialogueText.text = "Move Complete.";

        yield return new WaitForSeconds(1f);


        playerPosition = playerPosition + 1;

        //update player elevation


        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }


}
