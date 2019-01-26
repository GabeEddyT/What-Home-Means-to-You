using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    public float rage;
    public bool invulnerable;
    public bool parryFrames;
    public float parryLength;
    public float parryRecovery;
    public float parryFreezeFrameLength;
    public float hitStunLength;

    public GameObject parryParticle;

    bool canParry = true;

    public AudioClip parrySound;
    public AudioClip damageSound;

    AudioSource audioSource;
    ControlButtons buttonSuite;
    PlayerMovement movementScript;
    ItemManager throwScript;
    Coroutine parryRoutine;

    private void Start()
    {
        buttonSuite = GetComponent<ControlButtons>();
        movementScript = GetComponent<PlayerMovement>();
        throwScript = GetComponent<ItemManager>();
        audioSource = GetComponent<AudioSource>();
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

            Instantiate(parryParticle, transform.position, transform.rotation);

            audioSource.PlayOneShot(parrySound);

            StopCoroutine(parryRoutine);
            endParry();

            StartCoroutine(Pause());
        }
        else
            takeDamage(throwableScript.damage);
    }

    void takeDamage(float damage)
    {
        rage += damage;

        rage = Mathf.Min(rage, 100f);

        audioSource.PlayOneShot(damageSound);

        StartCoroutine(hitStun());
    }


    IEnumerator hitStun()
    {
        if (parryRoutine != null)
        {
            StopCoroutine(parryRoutine);
            endParry();
        }

        movementScript.canMove = false;
        throwScript.canThrow = false;
        canParry = false;

        yield return new WaitForSeconds(hitStunLength);

        movementScript.canMove = true;
        throwScript.canThrow = true;
        canParry = true;
    }

    //IEnumerator damageFlash()
    //{

    //}

    IEnumerator doParry()
    {
        parryFrames = true;
        movementScript.canMove = false;
        throwScript.canThrow = false;
        canParry = false;

        yield return new WaitForSeconds(parryLength);

        parryFrames = false;

        yield return new WaitForSeconds(parryRecovery);

        endParry();
    }
    private IEnumerator Pause()
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + parryFreezeFrameLength;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }


    void endParry()
    {
        canParry = true;

        parryFrames = false;

        movementScript.canMove = true;
        throwScript.canThrow = true;
        parryRoutine = null;
    }

}
