using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerInput : Sounds
{
    public static Action OnJump;

    public float MoveInput { get; private set; }
    public float RotationInput { get; private set; }

    [SerializeField] private int coins;
    [SerializeField] private Text coinsText;


    [SerializeField] private Joystick _joystick;


    private void Update()
    {

        if (YG2.envir.isMobile)
        {
            if (_joystick.Vertical > 0)
            {
                MoveInput = 1;

            }
            else if (_joystick.Vertical < 0)
            {
                MoveInput = -1;
            }
            else
            {
                MoveInput = 0;
            }
            RotationInput = _joystick.Horizontal;
           

        }
        else
        {
            MoveInput = Input.GetAxis("Vertical");
            RotationInput = Input.GetAxis("Horizontal");


        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump() // прыжок
    {
        OnJump?.Invoke();
        PlaySound(sounds[0], 0.85f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin") // кушание
        {
            coins++;
            PlaySound(sounds[1]);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }
}
