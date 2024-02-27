using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProcessComtrol : MonoBehaviour
{
    public Image process;
    public float currentValue = 0f;
    public float timer;
    float a = 0;
    public ParticleSystem particle;
    public ParticleSystem particle1;
    // Start is called before the first frame update
    void Start()
    {
        process = transform.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
         a = timer / 6;
        timer += Time.deltaTime;
        if (a >= 1f)
        {
            a = 6f;
            Destroy(transform.gameObject);
            particle.transform.gameObject.SetActive(true);
            particle1.transform.gameObject.SetActive(false );
        }
        SetProgress(a );
        if (a >0&&a < 1)
        {
            particle1.transform.gameObject.SetActive(true);
            particle.transform.gameObject.SetActive(false );
        }
    }
    // 设置进度条的显示
    public void SetProgress(float value)
    {
       
        transform .GetChild(0).transform .GetComponent<Image>().fillAmount = value;
    }
}
