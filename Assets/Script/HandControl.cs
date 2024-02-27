using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandControl : MonoBehaviour
{
    public GameObject  IM;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Des());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Des()
    {
        yield return new WaitForSeconds(1f);
        Destroy(transform.gameObject);
        IM.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}
