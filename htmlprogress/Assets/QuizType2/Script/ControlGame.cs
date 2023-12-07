using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [Header("String Keterangan Gambar")]
    public string[] stringImageSoal;
    public string[] splitStringImageSoal;
    public int[] lenghtPerText;
    public int indexTextPanjang;

    [Header("Box Kata")]
    public Transform perentKata;
    public GameObject prefabBoxKata;
    public float exstraSpace;

    [Header("Highlight Target Text")]
    public int indexPositionHighlight;
    public GameObject[] cloneBoxKatas;

    [Header("potongan kata")]
    public int[] indexRandomPotonganKata;
    public Text textPotonganKata;
    public int countKata;
    public RectTransform rtPotonganKata;
    public float extraSpacePotonganKata;

    [Header("Move Potongan Kata")]
    public GameObject potonganKata;
    public float speedMovementPotonganKata;
    public bool IsStarMovePotonganKata;
    Vector3 firstPositionPotonganKata;
    public bool[] isKataBenar;
    public GameObject coverBlockMovement;

    private void Start()
    {      

        RandomImageSoal();

        GeneratPotonganKata();

        firstPositionPotonganKata = potonganKata.transform.position; //save posisi awal

        isKataBenar = new bool[splitStringImageSoal.Length];//create slot array
    }

    private void Update()
    {
        MovementPotonganKata();
    }

    public void ButtonPotonganKata()
    {
        if(isKataBenar[indexPositionHighlight] == false)
        {
            IsStarMovePotonganKata = true;

            coverBlockMovement.SetActive(true);//aktif
        }
    }

    void MovementPotonganKata()
    {
        if(IsStarMovePotonganKata == true)
        {
            potonganKata.transform.position = Vector3.MoveTowards(potonganKata.transform.position, cloneBoxKatas[indexPositionHighlight].transform.position, Time.deltaTime * speedMovementPotonganKata);// move object

            if (Vector3.Distance(potonganKata.transform.position, cloneBoxKatas[indexPositionHighlight].transform.position) < 0.001f)
            {
                Debug.Log("Text Sampai Ketujuan");
                coverBlockMovement.SetActive(false);//mati

                potonganKata.transform.position = firstPositionPotonganKata;//reset position


                if (textPotonganKata.text == splitStringImageSoal[indexPositionHighlight])//Index Posisi - count kata respons
                {
                    Debug.Log(textPotonganKata.text + " ? " + splitStringImageSoal[indexPositionHighlight]);
                    Debug.Log("Jawaban Benar");

                    isKataBenar[indexPositionHighlight] = true;//merubah bool atau kondisi box text

                    cloneBoxKatas[indexPositionHighlight].transform.GetChild(0).GetComponent<Text>().text = textPotonganKata.text;//change text dengan jawaban

                    if(countKata < splitStringImageSoal.Length - 1)
                    {
                        countKata += 1;//increase karena benar

                        textPotonganKata.text = splitStringImageSoal[indexRandomPotonganKata[countKata]];//update text kata

                        rtPotonganKata.sizeDelta = new Vector2(textPotonganKata.preferredWidth + extraSpacePotonganKata, rtPotonganKata.sizeDelta.y); //resize box

                    }
                    else
                    {
                        Debug.Log("Generate Level Berikutnya");
                    }
                }
                else
                {
                    Debug.Log("Jawaban Salah");
                }


                IsStarMovePotonganKata = false;
            }
        }

        
    }

    void GeneratPotonganKata()
    {
        indexRandomPotonganKata = new int[splitStringImageSoal.Length];//create slot

        for (int i = 0; i < indexRandomPotonganKata.Length; i++)
        {
            indexRandomPotonganKata[i] = i;//fill array
        }

        RandomValue(indexRandomPotonganKata);//random index

        textPotonganKata.text = splitStringImageSoal[indexRandomPotonganKata[countKata]]; //update UI Text

        rtPotonganKata.sizeDelta = new Vector2(textPotonganKata.preferredWidth + extraSpacePotonganKata, rtPotonganKata.sizeDelta.y); //resize box
    }

    public void ButtonHighlight()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "left")
        {
            indexPositionHighlight -= 1;
            if (indexPositionHighlight < 0)
            {
                indexPositionHighlight = cloneBoxKatas.Length - 1; //lastElement
            }
        }
        else//right
        {
            indexPositionHighlight += 1;
            if (indexPositionHighlight > splitStringImageSoal.Length - 1)
            {
                indexPositionHighlight = 0; //firstElement
            }
        }

        for (int i = 0; i < cloneBoxKatas.Length; i++)
        {
            if (i != indexPositionHighlight)
            {
                cloneBoxKatas[i].transform.GetChild(1).gameObject.SetActive(false);//matikan semua
            }
            else
            {
                cloneBoxKatas[i].transform.GetChild(1).gameObject.SetActive(true);//hidupkan semua
            }
        }

    }

    void RandomImageSoal()
    {
        indexRandomSprites = new int[spriteSoals.Length];
        for (int i = 0; i < indexRandomSprites.Length; i++)
        {
            indexRandomSprites[i] = i;
        }
        if (isRandomSprites == true)
        {
            RandomValue(indexRandomSprites);// index random
        }
        ImageSoal.sprite = spriteSoals[indexRandomSprites[gameRound]];//implementasi sprite gambar

        //implementasi keterangan gambar
        splitStringImageSoal = stringImageSoal[indexRandomSprites[gameRound]].Split(' ');//dipotong dengan acuan spasi

        lenghtPerText = new int[splitStringImageSoal.Length];//create slot

        for (int i = 0; i < lenghtPerText.Length; i++)
        {
            lenghtPerText[i] = splitStringImageSoal[i].Length;//diisi dari length text
        }

        for (int i = 0; i < lenghtPerText.Length; i++)
        {
            if (lenghtPerText[i] == Mathf.Max(lenghtPerText))
            {
                indexTextPanjang = i; //index text terpanjang
            }
        }

        cloneBoxKatas = new GameObject[splitStringImageSoal.Length];//create slot array

        //respons
        for (int i = 0; i < splitStringImageSoal.Length; i++)
        {
            GameObject cloneBoxKata = Instantiate(prefabBoxKata);// respawn
            cloneBoxKata.transform.SetParent(perentKata);// set parent

            cloneBoxKatas[i] = cloneBoxKata; //fiill array

            if (i == 0)// for change size x
            {
                Text textTerpanjang = cloneBoxKata.transform.GetChild(0).GetComponent<Text>();
                textTerpanjang.text = splitStringImageSoal[indexTextPanjang];

                Debug.Log(textTerpanjang.flexibleWidth + "/" + textTerpanjang.preferredWidth);

                perentKata.GetComponent<GridLayoutGroup>().cellSize = new Vector2(textTerpanjang.preferredWidth + exstraSpace, perentKata.GetComponent<GridLayoutGroup>().cellSize.y);

                cloneBoxKata.transform.GetChild(1).gameObject.SetActive(true);// aktifkan highlight
            }

            Text textCloneBoxKata = cloneBoxKata.transform.GetChild(0).GetComponent<Text>();

            textCloneBoxKata.text = "";

            for (int j = 0; j < splitStringImageSoal[i].Length; j++)
            {
                textCloneBoxKata.text += "_";
            }
        }


    }
    void RandomValue(int[] indexRandoms)
    {
        for (int i = 0; i < indexRandoms.Length; i++)
        {
            int a = indexRandoms[i];
            int b = Random.Range(0, indexRandoms.Length);
            indexRandoms[i] = indexRandoms[b];
            indexRandoms[b] = a;
        }
    }

    void RandomValueString(string[] indexRandoms)
    {
        for (int i = 0; i < indexRandoms.Length; i++)
        {
            string a = indexRandoms[i];
            int b = Random.Range(0, indexRandoms.Length);
            indexRandoms[i] = indexRandoms[b];
            indexRandoms[b] = a;
        }
    }
}