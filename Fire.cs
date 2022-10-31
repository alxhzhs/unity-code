using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fire : MonoBehaviour
{
    public GameManagerLogic Total;
    public GameObject effect;
    public Transform spPoint;
    public Transform bullet;
    private Transform target;
    public Text ScriptTex;
    public Text fullammo;
    AudioSource[] gunSound;
    public int count = 0;
    public int count2 = 0;
    private int mixammo = 0;
    float timer;
    float waitingTime;

    RaycastHit hit;
    public LayerMask Layermask;

    private int mask;

    public GameObject cam;

    float CamMaxLine = 160f;

    bool canfire = true;
    bool autoC = false;
    bool canreload = true;

    [SerializeField]
    private GameObject hit_enermy_prefab;
    [SerializeField]
    private GameObject hit_nt_enermy_prefab;
    [SerializeField]
    private GameObject hit_effect;
    void Start()
    {
        Cursor.visible = false;                 //커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        count = 30;                             // 총알
        ScriptTex.text = count.ToString();      // 총알 텍스트
        count2 = 150;                           // 총알
        fullammo.text = count2.ToString();      // 총알 텍스트
        target = GameObject.Find("Player").transform;
        gunSound = GetComponents<AudioSource>();
        gunSound[0].volume = 0.1f;
        timer = 0.0f;
        waitingTime = 0.8f;
        Invoke("Invoke_mute", 0.8f);
        mask = (-1) - (1 << LayerMask.NameToLayer("nt"));
    }
    private void FixedUpdate()
    {
        Quaternion rot = target.rotation;
    }
    void Invoke_mute()
    {
        gunSound[0].mute = false;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            if(GameManagerLogic.game_stop == false)
            {
                Update_set();
            } 
        }
        if(Total.TotalCount == 30)
        {
            SceneManager.LoadScene("clearscnes");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if(ScoreManager.Score <= -100)
        {
            SceneManager.LoadScene("fallscnes");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
       
    }
    void Update_set()
    {
        if (SceneManager.GetActiveScene().name.CompareTo("mainloby") != 0)
        {
            if (Input.GetButtonDown("Fire1") && count > 0 && autoC == false)
            {
                SingleShut();
            }
            if (Input.GetButton("Fire1") && count > 0 && autoC == true && canfire == true)
            {
                StartCoroutine(AutoShut());
                if (!Input.GetButton("Fire1"))
                {
                    gunSound[0].Stop();
                }
            }
            if (Input.GetKeyDown("b") && autoC == false)
            {
                Debug.Log("오토");
                autoC = true;
            }
            else if (Input.GetKeyDown("b") && autoC == true)
            {
                Debug.Log("수동");
                autoC = false;
            }
            if (count2 > 0)
            {
                if (canreload)
                {
                    if (Input.GetKeyDown("r"))
                    {
                        StartCoroutine(ReloadCT());
                    }
                }
            }
            float x = Input.GetAxis("Mouse Y") * GameManagerLogic.up_down_DPI;
            Vector3 ang = spPoint.localEulerAngles + new Vector3(-x, 0, 0);
            if (ang.x > 180)
            {
                ang.x -= 360;
            }
            ang.x = Mathf.Clamp(ang.x, -80, 90);
            spPoint.localEulerAngles = ang;
        }
    }
    void SingleShut()
    {
        Instantiate(bullet, spPoint.position, spPoint.rotation);
        //Debug.DrawRay(cam.transform.position, cam.transform.forward * CamMaxLine, Color.red, 0.3f);       레이캐스트 표시
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, CamMaxLine, mask))
        {
            GameObject hiteffect = Instantiate(hit_effect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hiteffect, 1);
        }
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, CamMaxLine , Layermask))
        {
            GameObject hiteffect = Instantiate(hit_effect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hiteffect, 1);
            if (hit.collider.CompareTag("Enermy"))      //파란색 공을 맞혔을시 점수 올림
            {
                Destroy(hit.transform.gameObject);
                GameObject clone = Instantiate(hit_enermy_prefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(clone, 5);
                ScoreManager.Score += 10;
                GameManagerLogic.spawn_control = true;
                Total.TotalCount++;
            }
            if (hit.collider.CompareTag("nt_Enermy"))    //빨간색 공을 맞혔을시 점수 내림
            {
                Destroy(hit.transform.gameObject);
                GameObject clone = Instantiate(hit_nt_enermy_prefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(clone, 5);
                ScoreManager.Score -= 40;
                GameManagerLogic.spawn_control = true;
            }
        }
        Count_num();
        gunSound[0].Play();
        effect.SetActive(true);
    }

    IEnumerator AutoShut()
    {
        Instantiate(bullet, spPoint.position, spPoint.rotation);
        //Debug.DrawRay(cam.transform.position, cam.transform.forward * CamMaxLine, Color.red, 0.3f);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, CamMaxLine, mask))
        {
            GameObject hiteffect = Instantiate(hit_effect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hiteffect, 1);
        }
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, CamMaxLine, Layermask))
        {
            if (hit.collider.CompareTag("Enermy"))   //파란색 공을 맞혔을시 점수 올림
            {
                Destroy(hit.transform.gameObject);
                GameObject clone = Instantiate(hit_enermy_prefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(clone, 5);
                ScoreManager.Score += 10;
                GameManagerLogic.spawn_control = true;
                Total.TotalCount++;
            }
            if (hit.collider.CompareTag("nt_Enermy"))   //빨간색 공을 맞혔을시 점수 내림
            {
                Destroy(hit.transform.gameObject);
                GameObject clone = Instantiate(hit_nt_enermy_prefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(clone, 5);
                ScoreManager.Score -= 40;
                GameManagerLogic.spawn_control = true;
            }
        }
        Count_num();
        effect.SetActive(true);
        gunSound[0].Play();
        canfire = false;

        yield return new WaitForSeconds(0.1f);
        canfire = true;
    }
    IEnumerator ReloadCT()
    {
        canreload = false;
        mixammo = count + count2;
        Debug.Log("재장전");
        Debug.Log(count);
        yield return new WaitForSeconds(1.5f);
        Count2_num();
        if (count2 >= 30)
        {
            count = 30;
        }
        else if (count2 < 30 && mixammo < 30)
        {
            count = mixammo;
        }
        else
        {
            count = 30;
        }
        ScriptTex.text = count.ToString();
        canreload = true;
    }
    public void Count_check()
    {
        ScriptTex.text = count.ToString();
        fullammo.text = count2.ToString();

    }
    public void Count_num()
    {
        count -= 1;
        ScriptTex.text = count.ToString();
    }
    void Count2_num()
    {
        if (count2 >= 30)
        {
            count2 -= 30 - count;
        }
        else if(count2 < 30 && mixammo > 30)
        {
            count2 -= 30 - count;
        }
        else
        {
            count2 -= count2;
        }
        fullammo.text = count2.ToString();
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("ammo"))
        {
            count = 30;
            count2 = 150;
            Count_check();
            Destroy(col.gameObject);
        }
    }
}
