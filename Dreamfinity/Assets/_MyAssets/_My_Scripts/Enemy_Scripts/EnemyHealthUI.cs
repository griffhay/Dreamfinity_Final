using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthUI : MonoBehaviour
{  
    public Canvas canvasRef;
    public Slider sliderRef;
    public RectTransform rectTransRef;
    public GameObject camRef;

    public EnemyHealthManager enemyHealthManRef;
    public LucidityControl enemtLucCont;

    private void Start()
    {
        canvasRef = this.GetComponentInChildren<Canvas>();
        rectTransRef = canvasRef.GetComponent<RectTransform>();
        camRef = GameObject.FindWithTag("MainCamera");
        enemyHealthManRef =  GetComponentInParent<EnemyHealthManager>();
        sliderRef = canvasRef.GetComponentInChildren<Slider>();
        sliderRef.maxValue = enemyHealthManRef.helthMax;
    }

    private void Update()
    {
        Vector3 direction = camRef.transform.position - rectTransRef.position;
        direction.y = 0;
        rectTransRef.rotation = Quaternion.LookRotation(direction, rectTransRef.up);

        sliderRef.value = enemyHealthManRef.heathValue;
    }
}
