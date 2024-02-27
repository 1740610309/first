using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlenderControl : MonoBehaviour
{
    public GameObject B1;
    public GameObject B2;
    public float RoateSpeed;
    public GameObject TargetPoint;
    public Animator ani;
    public float timer;
    public float moveSpeed;
    public bool Istart = false;
    public GameObject Batter;
    public GameObject BatterPOS;
    private bool isDragging = false; // 是否正在拖拽物体
    private Vector3 offset; // 鼠标点击位置与物体中心的偏移量
    public GameObject vmc;
    public Image process;
    public GameObject IM;
  
    // Start is called before the first frame update
    void Start()
    {
        B1 = transform.GetChild(4).gameObject;
        B2 = transform.GetChild(5).gameObject;
        ani = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        // 如果鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            // 发射一条射线检测鼠标点击的物体
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 如果点击到了物体，开始拖拽
                if (hitInfo.transform.tag  == "Blender")
                {
                    isDragging = true;
                  
                }
            }
        }
        // 如果鼠标左键释放，停止拖拽
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 如果正在拖拽物体，更新物体位置
        if (isDragging)
        {
            // 获取鼠标点击位置
            Vector3 mousePosition = Input.mousePosition;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            // 将鼠标位置转换为世界坐标
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, pos .z  ));
            // 更新物体位置
            transform.position = worldPosition;
            
        }

        if (Istart)//开始搅拌
        {
            ChildernRoate();
            timer += Time.deltaTime;
            MoveTarget();
            if (timer > 1.5f)
            {
                ParentRoate();
            }
            if (timer > 6f)
            {
                Destroy(transform.gameObject);
            }
            process.color = new Color(255, 255, 255, 255);
            process.transform.GetComponent<ProcessComtrol>().enabled = true;
            
        }

    }
    public void ChildernRoate()//子物体旋转
    {
        B1.transform.Rotate(Vector3.up, Time.deltaTime * RoateSpeed);
        B2.transform.Rotate(Vector3.up, Time.deltaTime * RoateSpeed);
    }
    public void ParentRoate()//播放搅拌动画
    {
        ani.enabled = true;
    }
    public void MoveTarget()//播放贴图动画
    {
        Batter.transform.GetComponent<Animator>().enabled = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bowl"))
        {
          
            isDragging = false;
            transform.position = TargetPoint.transform.position;
            vmc.transform.gameObject.SetActive(true);
            StartCoroutine(GetStart());
            IM.transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }
    public IEnumerator GetStart()
    {
        yield return new WaitForSeconds(2f);
        Istart = true;
        
    }
}
