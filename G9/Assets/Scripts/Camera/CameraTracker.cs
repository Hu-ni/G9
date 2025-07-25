using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    // 
    // 카메라 추적 Scripts
    // Obj를 바라보게 하는 Script
    // 생각보다 필요 없을지도?
    [SerializeField]
    private GameObject Obj;

    private void Update()
    {
        transform.LookAt(Obj.transform.position);
    }
}