using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // ���� ���� �ӵ�
    public float speed = 1.0f;

    // ���� ����� �� ����
    public Color startColor = Color.red;
    public Color endColor = Color.blue;

    private Renderer rend; // ������ ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); // ������ ������Ʈ ��������
    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.PingPong �Լ��� ����Ͽ� �ð��� ���� 0�� 1 ���̸� �պ��ϴ� ���� ���մϴ�.
        float lerpValue = Mathf.PingPong(Time.time * speed, 1);

        // Color.Lerp �Լ��� ����Ͽ� startColor�� endColor ������ ������ �����մϴ�.
        rend.material.color = Color.Lerp(startColor, endColor, lerpValue);
    }
}
