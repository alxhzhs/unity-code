using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_target : MonoBehaviour
{
    //카메라 위치, 각도, FOV ---------
    /*[SerializeField] Vector3 position = new Vector3(-1f, 3.6f, -7.8f);
    [SerializeField] Vector3 rotation = new Vector3(12f, 0, -0f);*/
    [SerializeField] Vector3 position = new Vector3(-0, 0, -0);
    [SerializeField] Vector3 rotation = new Vector3(0, 0, -0f);
    [SerializeField][Range(10, 100)]
    float fov = 30f;

    //카메라 이동 및 회전속도 ---------
    float moveSpeed = 10f;
    float turnSpeed = 10f;
   // float rot = 0.0f;

    public Transform target;
    Transform cam;
    Transform pivot;
    Transform pivotRot;

    // Start is called before the first frame update
    void Start()
    {
        InitCamera();
    }

    void InitCamera()
    {
        cam = Camera.main.transform;
        cam.GetComponent<Camera>().fieldOfView = fov;

        pivot = new GameObject("pivot").transform;
        pivot.position = new Vector3(0, 1.5f, 0);
        pivot.position = target.position;

        pivotRot = new GameObject("pivotRot").transform;
        pivotRot.position = new Vector3(0, 1.5f, 0);
        pivotRot.parent = pivot;

        cam.parent = pivotRot;
        cam.localPosition = position;
        cam.localEulerAngles = rotation;

    }

    private void FixedUpdate()
    {
        if(GameManagerLogic.game_stop == false) 
        {
            Vector3 pos = target.position;
            Quaternion rot = target.rotation;

            pivot.position = Vector3.Lerp(pivot.position, pos, moveSpeed * Time.deltaTime);
            //pivot.rotation에 pos값을 대입한다
            pivot.rotation = Quaternion.Lerp(pivot.rotation, rot, turnSpeed * Time.deltaTime);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerLogic.game_stop == false)
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * 20;   //-20 ~ 20
            fov = Mathf.Clamp(fov - zoom, 70, 120);
            cam.GetComponent<Camera>().fieldOfView = fov;

            float x = Input.GetAxis("Mouse Y") * GameManagerLogic.up_down_DPI;

            /*rot = -pivotRot.localRotation.x * 40.0f;
            anim.SetFloat("up_down", rot);*/

            Vector3 ang = pivotRot.localEulerAngles + new Vector3(-x, 0, 0);
            if (ang.x > 180)
            {
                ang.x -= 360;
            }
            ang.x = Mathf.Clamp(ang.x, -80, 90);
            pivotRot.localEulerAngles = ang;
        }
    }
}
