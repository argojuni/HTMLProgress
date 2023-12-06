using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBack : MonoBehaviour
{
    public GameObject feedBackCorrect,feedBackFalse;

    bool selesai = false;

    public int countPuzzle;

    private void Update()
    {
        if (!selesai)
        {
            cek();
        }
    }

    public void cek()
    {
        for(int i=0; i<countPuzzle; i++)
        {
            if (transform.GetChild(i).GetComponent<Drag>().on_tempel)
            {
                selesai = true;
            }
            else
            {
                selesai = false;
                i = countPuzzle;
            }
        }
        if (selesai)
        {
            feedBackCorrect.SetActive(true);
            feedBackFalse.SetActive(false);
        }
    }
}
