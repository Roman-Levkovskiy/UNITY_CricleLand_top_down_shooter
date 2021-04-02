using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    public void showControls()
    {
        gameObject.SetActive(true);
    }

    public void hideControls()
    {
        gameObject.SetActive(false);
    }
}
