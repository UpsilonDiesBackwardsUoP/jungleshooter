using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    [Header("Reference")]
    public AudioSource audioSource;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _graphic;
    [SerializeField] private Component[] _graphicSprites;
    [SerializeField] private Rigidbody2D rb;

    // Singleton
    private static NewPlayer instance;
    public static NewPlayer Instance {
        get {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    [Header("Properties")]
    public bool dead = false;
    public bool frozen = false;
    [System.NonSerialized] public string groundType = "grass";
    [System.NonSerialized] public RaycastHit2D ground;
    [SerializeField] Vector2 _hurtLaunchPower;
    public float maxSpeed = 7f;
    public float jumpPower = 17;
    private bool jumping;
    private Vector3 origLocalScale;

    [Header("Inventory")]
    public int gold;
    public int health;
    public int maxHealth;

    [Header("Sounds")]
    public AudioClip grassSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip stepSound;
    [System.NonSerialized] public int whichHurtSound;
    
    void Start() {
        Cursor.visible = false;
        health = maxHealth;
        origLocalScale = transform.localScale;

        _graphicSprites = GetComponentsInChildren<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        SetGroundType();
    }

    private void Update() {
        ComputeVelocity();
    }

    protected void ComputeVelocity() {
        Vector3 move = Vector3.zero;
        ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector3.up);

        if (!frozen) {
            move.x = Input.GetAxis("HorizontalDirection");
            move.z = Input.GetAxis("VerticalDirection");

            //Flip the graphic's localScale
            if (move.x > 0.01f)
            {
                _graphic.transform.localScale = new Vector3(origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (move.x < -0.01f)
            {
                _graphic.transform.localScale = new Vector3(-origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }   

            _animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x) / maxSpeed);
            _animator.SetFloat("velocityY", rb.velocity.y);
            _animator.SetInteger("moveDirection", (int)Input.GetAxis("HorizontalDirection"));

            rb.velocity = new Vector2(move.x * maxSpeed, move.z * maxSpeed);
        }
    }

    public void SetGroundType() {
        switch (groundType) {
            case "grass":
            stepSound = grassSound;
            break;
        }
    }

    public void Freeze(bool freeze)
    {
        if (freeze)
        {
            _animator.SetInteger("moveDirection", 0);
            _animator.SetBool("grounded", true);
            _animator.SetFloat("velocityX", 0f);
            _animator.SetFloat("velocityY", 0f);
        }

        frozen = freeze;
    }

    public void PlayStepSound() {
        audioSource.pitch = (Random.Range(0.9f, 1.1f));
        audioSource.PlayOneShot(stepSound, Mathf.Abs(Input.GetAxis("Horizontal") / 10));
    }

    public void PlayJumpSound()
    {
        audioSource.pitch = (Random.Range(1f, 1f));
        // GameManager.Instance.audioSource.PlayOneShot(jumpSound, 0.1f);
    }

    public void JumpEffect()
    {
        audioSource.pitch = (Random.Range(0.6f, 1f));
        audioSource.PlayOneShot(landSound);
    }

    public void LandEffect()
    {
        if (jumping) {
            audioSource.pitch = (Random.Range(0.6f, 1f));
            audioSource.PlayOneShot(landSound);
            Debug.Log("Jump Sound"); 

            jumping = false;
        }
    }

    public void Hide(bool hide)
    {
        Freeze(hide);
        foreach (SpriteRenderer sprite in _graphicSprites)
            sprite.gameObject.SetActive(!hide);
    }
}
