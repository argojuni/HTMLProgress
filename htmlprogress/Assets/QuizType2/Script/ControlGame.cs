using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlGame : MonoBehaviour
{
    public int gameRound;

    [Header("Image Question")]
    public Image ImageSoal;

    public Sprite[] spriteSoals;

    public int[] indexRandomSprites;

    [Tooltip("jika ingin random centang ini")]
    public bool isRandomSprites;

    private void Start()
    {
        RandomImageSoal();
    }

    void RandomImageSoal()
    {
        indexRandomSprites = new int[spriteSoals.Length];

        for(int i = 0; i <indexRandomSprites.Length; i++)
        {
            indexRandomSprites[i] = i;
        }

        if(isRandomSprites == true)
        {
            RandomValue(indexRandomSprites);// index random
        }
        ImageSoal.sprite = spriteSoals[indexRandomSprites[gameRound]];//implementasi sprite gambar
    }

    void RandomValue(int[] indexRandoms)
    {
        for(int i = 0; i<indexRandoms.Length; i++)
        {
            int a = indexRandoms[i];
            int b = Random.Range(0, indexRandoms.Length);
            indexRandoms[i] = indexRandoms[b];
            indexRandoms[b] = a;

        }
    }
}
