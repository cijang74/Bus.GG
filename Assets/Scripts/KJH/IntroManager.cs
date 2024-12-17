using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private DialogueUIController DefaultDialogueController;
    [SerializeField] private DialogueUIController DefaultDialogueController2;

    [SerializeField] public float moveSpeed;
    [SerializeField] public int targetPositionX;

    Animator animator;
    NoInstanceFade noInstanceFade;

    private GameObject playerobj;
    private bool moveFlag = false;
    private bool endFlag = false;

    private void Awake() 
    {
        animator = FindAnyObjectByType<Animator>();
        noInstanceFade = GetComponent<NoInstanceFade>();

    }
    
    private void FixedUpdate() 
    {
        if(moveFlag && !endFlag)
        {
            playerobj.transform.position = new Vector3(playerobj.transform.position.x + moveSpeed, playerobj.transform.position.y,  playerobj.transform.position.z);

            if(playerobj.transform.position.x >= targetPositionX)
            {
                DefaultDialogueController2.ShowDialogueUI();
                animator.SetBool("isRun", false);
                moveFlag = false;
                endFlag = true;
            }
        }

        if(moveFlag && endFlag)
        {
            playerobj.transform.position = new Vector3(playerobj.transform.position.x + moveSpeed, playerobj.transform.position.y,  playerobj.transform.position.z);

            if(playerobj.transform.position.x >= 52)
            {
                noInstanceFade.FadeToBlack();
                animator.SetBool("isRun", false);
                moveFlag = false;
            }
        }
    }

    void Start()
    {
        DefaultDialogueController.ShowDialogueUI();

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("Player"))
            {
                playerobj = obj;
            }
        }
    }

    public void MoveForward()
    {
        Debug.Log("실행됨");
        if(playerobj.transform.position.x >= 40)
        {
            targetPositionX = 52;
        }

        DefaultDialogueController.HideDialogueUI();
        animator.SetBool("isRun", true);
        moveFlag = true;
    }
}
