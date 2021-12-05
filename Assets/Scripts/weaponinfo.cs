using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weaponinfo : MonoBehaviour
{
    public TMP_Text currentBullet;
    public TMP_Text totalBullets;
    // Start is called before the first frame update
    private void OnEnable()
    {
        eventmanager.current.UpdateBulletsEvent.AddListener(UpdateBullets);
    }

    private void OnDisable()
    {
        eventmanager.current.UpdateBulletsEvent.RemoveListener(UpdateBullets);

    }

    void UpdateBullets(int newCurrentBullets, int newTotalBullets)
    {
        if (newCurrentBullets <= 0)
        {
            currentBullet.color = Color.red;
        }
        else
        {
            currentBullet.color = Color.white;
        }
        currentBullet.text = newCurrentBullets.ToString();
        totalBullets.text = newTotalBullets.ToString();
    }

}
