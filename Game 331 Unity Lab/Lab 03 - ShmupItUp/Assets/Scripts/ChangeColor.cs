using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // 색상 변경 속도
    public float speed = 1.0f;

    // 시작 색상과 끝 색상
    public Color startColor = Color.red;
    public Color endColor = Color.blue;

    private Renderer rend; // 렌더러 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); // 렌더러 컴포넌트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.PingPong 함수를 사용하여 시간에 따라 0과 1 사이를 왕복하는 값을 구합니다.
        float lerpValue = Mathf.PingPong(Time.time * speed, 1);

        // Color.Lerp 함수를 사용하여 startColor와 endColor 사이의 색상을 보간합니다.
        rend.material.color = Color.Lerp(startColor, endColor, lerpValue);
    }
}
