using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    // Start is called before the first frame update
    //오브젝트가 활성화 될때 호출
    /*   private void OnEnable()
       {
           //0.09초 활성화 되고 비활성화 됨
           //지정한 함수를 지시한 후에 호출, 함수이름은 문자열
           Invoke("Disable", 0.09f);
       }
       private void OnDisable()
       {
           gameObject.SetActive(false);   
       }*/

    public void OnEnable()
    {
        StartCoroutine(Disable(0.03f));
    }
    IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
