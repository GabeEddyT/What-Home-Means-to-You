using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageScript : MonoBehaviour
{
    public float rage;
    public bool invulnerable;
    public bool parryFrames;
    public float parryLength;
    public float parryRecovery;
    public float parryFreezeFrameLength;
    public float hitStunLength;

    public float parryEffectLifetime;
    //public float flashDelay;
    public GameObject parryParticle;

    bool canParry = true;
    bool inHitStun = false;

    public AudioClip parrySound;
    public AudioClip damageSound;

    AudioSource audioSource;
    ControlButtons buttonSuite;
    PlayerMovement movementScript;
    ItemManager throwScript;
    Coroutine parryRoutine;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public PlayerRageUI PlayerRageUIObj;
    private void Start()
    {
        buttonSuite = GetComponent<ControlButtons>();
        movementScript = GetComponent<PlayerMovement>();
        throwScript = GetComponent<ItemManager>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canParry && Input.GetKeyDown(buttonSuite.parry))
            parryRoutine = StartCoroutine(doParry());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ThrowableObject throwableScript = collision.gameObject.GetComponent<ThrowableObject>();

        if (!throwableScript)
            return;

        if (parryFrames) 
        {         
            throwableScript.Deflect(GetComponent<Collider2D>());

            GameObject parryEffect = Instantiate(parryParticle, transform.position, transform.rotation);
            Destroy(parryEffect, parryEffectLifetime);

            audioSource.PlayOneShot(parrySound);

            StopCoroutine(parryRoutine);
            endParry();

            StartCoroutine(PauseScreen());
        }
        else if(!inHitStun)
            takeDamage(throwableScript.damage);
    }

    void takeDamage(float damage)
    {
        rage += damage;

        rage = Mathf.Min(rage, 100f);

        audioSource.PlayOneShot(damageSound);

        //PlayerRageUIObj.AddRage(damage, this.gameObject);

        StartCoroutine(hitStun());
    }


    IEnumerator hitStun()
    {
        if (parryRoutine != null)
        {
            StopCoroutine(parryRoutine);
            endParry();
        }
        animator.Play("Damage");

        movementScript.canMove = false;
        throwScript.canThrow = false;
        canParry = false;

        inHitStun = true;
        StartCoroutine(damageFlash());

        yield return new WaitForSeconds(hitStunLength);

        inHitStun = false;
        movementScript.canMove = true;
        throwScript.canThrow = true;
        canParry = true;
        animator.Play("Idle");

    }

    IEnumerator damageFlash()
    {
        while(inHitStun)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return null;
        }

        spriteRenderer.enabled = true;

        yield return null;
    }

    IEnumerator doParry()
    {
        parryFrames = true;
        movementScript.canMove = false;
        throwScript.canThrow = false;
        canParry = false;

        animator.Play("Parry");

        yield return new WaitForSeconds(parryLength);

        endParry();

        canParry = false;

        yield return new WaitForSeconds(parryRecovery);

        canParry = true;
    }
    private IEnumerator PauseScreen()
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + parryFreezeFrameLength;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }

        Time.timeScale = 1;

        //Camera.main.GetComponent<TwoPlayerCameraLogic>().beginShaking(0.3f);
    }


    void endParry()
    {
        canParry = true;

        parryFrames = false;

        movementScript.canMove = true;
        throwScript.canThrow = true;
        parryRoutine = null;

        animator.Play("Idle");
    }

}
