using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Missile")
        {
            other.gameObject.GetComponent<Missile>().HasEnteredSpeedZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Missile")
        {
            if (other.gameObject.GetComponent<Missile>().IsRepeatableSpeedZone)
            {
                other.gameObject.GetComponent<Missile>().HasEnteredSpeedZone = false;
            }
        }
    }
}
