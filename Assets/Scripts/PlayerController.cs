using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private const string SPEED_MULTIPLIER = "Speed Multiplier";
    private const string SPEED_F = "Speed_f";
    private const string JUMP_TRIG = "Jump_trig";
    private const string DEATH_TYPE = "DeathType_int";
    private const string IS_DEAD = "Death_b";


    private Rigidbody playerRb;

    [SerializeField]
    private float jumpForce = 800f;

    private bool isOnGround = true;

    [SerializeField]
    private float gravityMultiplier = 1f;

    private bool _gameOver = false;

    private Animator _animator;

    private float speedMultiplier = 1f;

    private float speedF = 1.0f;

    public ParticleSystem _explosion;

    public ParticleSystem _trace;

    public AudioClip jumpSound, crashSound;

    // Necesito la fuente para reproducir los "AudioClip"
    private AudioSource _audioSource;

    // Getters y setters
    public bool GameOver { get => _gameOver; }



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        // Añadido para modificar la gravedad al inicio
        Physics.gravity *= gravityMultiplier;

        _animator = GetComponent<Animator>();

        _animator.SetFloat(SPEED_F, speedF);

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime / 10;
        // Setea la velocidad de la animación de movimiento respecto al tiempo que pasó de juego
        //_animator.SetFloat(SPEED_F, 1);
        _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier);


        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !_gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // F = m * a

            isOnGround = false;

            _trace.Stop();

            // Pongo speed en 0 porque sino el personaje salta hacia arriba y adelante
            _animator.SetFloat(SPEED_F, 0);

            _animator.SetTrigger(JUMP_TRIG);

            _audioSource.PlayOneShot(jumpSound, 1);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_gameOver)
        {
            isOnGround = true;

            _trace.Play();

            _animator.SetFloat(SPEED_F, 1);
        }
        
        if(other.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true;

            _explosion.Play();

            Destroy(other.gameObject);

            Debug.Log("Game over");

            _animator.SetInteger(DEATH_TYPE, Random.Range(1, 3));

            _animator.SetBool(IS_DEAD, true);

            _trace.Stop();

            _audioSource.PlayOneShot(crashSound, 1);

            Invoke("RestartGame", 2f);
        }

    }

    void RestartGame()
    {
        speedMultiplier = 1;
        SceneManager.UnloadSceneAsync("Prototype 3");
        SceneManager.LoadScene("Prototype 3");
    }
}
