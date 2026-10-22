using UnityEngine;
using UnityEngine.UI;
public class StaminaWheel : MonoBehaviour
{
    private float stamina;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private Image greenwheel;
    [SerializeField] private Image redwheel;

    private bool staminaExhausted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    { 
        while (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space)) {
            if (Input.GetKey(KeyCode.LeftShift) && !staminaExhausted)
            {
                if (stamina > 0)
                {
                    stamina -= 30 * Time.deltaTime;
                }
                else
                {
                    staminaExhausted = true;
                }

                redwheel.fillAmount = (stamina / maxStamina + 0.07f);
            }
            else
            {
                if (stamina < maxStamina)
                {
                    stamina += 30 * Time.deltaTime;
                }
                else
                {
                    staminaExhausted = false;
                }
                redwheel.fillAmount = (stamina / maxStamina);
            }

            greenwheel.fillAmount = (stamina / maxStamina);
        }
    }
}
