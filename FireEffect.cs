using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    // Start is called before the first frame update
    //������Ʈ�� Ȱ��ȭ �ɶ� ȣ��
    /*   private void OnEnable()
       {
           //0.09�� Ȱ��ȭ �ǰ� ��Ȱ��ȭ ��
           //������ �Լ��� ������ �Ŀ� ȣ��, �Լ��̸��� ���ڿ�
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
