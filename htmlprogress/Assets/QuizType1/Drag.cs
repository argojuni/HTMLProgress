using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public GameObject detector,pos;
    
    Vector3 pos_awal,scale_awal;

    public FeedBack feedBack;

    public bool on_pos = false, on_tempel = false;
    private void Start()
    {
        pos_awal = transform.position;
        scale_awal = transform.localScale;
    }

    private void OnMouseDrag()
    {
        Vector3 pos_mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        transform.position = new Vector3(pos_mouse.x, pos_mouse.y, -1f);

        transform.localScale = pos.transform.localScale;
    }

    private void OnMouseUp()
    {
        if (on_pos)
        {
            transform.position = detector.transform.position;
            transform.localScale = pos.transform.localScale;
            on_tempel = true;
            feedBack.feedBackFalse.SetActive(false);
        }
        else
        {
            transform.position = pos_awal;
            transform.localScale = scale_awal;
            on_tempel = false;
            feedBack.feedBackFalse.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == detector)
        {
            on_pos = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == detector)
        {
            on_pos = false;
        }
    }
}
