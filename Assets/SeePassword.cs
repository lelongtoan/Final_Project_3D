using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeePassword : MonoBehaviour
{
    [SerializeField] GameObject seeOn;
    [SerializeField] GameObject seeOff;
    [SerializeField] InputField password;
    void Update()
    {
        if (password != null)
        {
            if (password.contentType == InputField.ContentType.Password)
            {
                seeOn.SetActive(true);
                seeOff.SetActive(false);
            }
            else
            {
                seeOff.SetActive(true);
                seeOn.SetActive(false);
            }
        }
    }
}
