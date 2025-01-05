using UnityEngine;
using YG;

public class MobilePlatform : MonoBehaviour
{
    public GameObject _inputMobile;
    private void Start()
    {
        _inputMobile.SetActive(YG2.envir.isMobile);
    }
}
