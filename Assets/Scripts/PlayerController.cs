using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float resetSpeed;
    public float dashSpeed;
    public float dashTime;
    PhotonView view;
    Animator animator;
    Health healthScript;
    public TextMeshProUGUI nickName;

    LineRenderer rend;
    void Start()
    {
        resetSpeed = speed;
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>();

        if(view.IsMine)
        {
            nickName.text = PhotonNetwork.NickName;
        }
        else{
            nickName.text = view.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            if(Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                StartCoroutine(Dash());
            }


            if(moveInput == Vector2.zero)
            {
                animator.SetBool("IsRunning",false);
            }
            else
            {
                animator.SetBool("IsRunning",true);
            }

            rend.SetPosition(0,transform.position);
        }
        else{
            rend.SetPosition(1,transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(view.IsMine)
        {
            if(other.tag == "Enemy")
            {
                healthScript.TakeDamage();
            }
        }
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }
}
