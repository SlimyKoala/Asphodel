using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagment : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float dampCoef = 0.1f;
    [SerializeField] float mouseLookControl = 0.2f;

    [SerializeField] float shakeCoef;
    [SerializeField] float maxShakeCoef;
    [SerializeField] float shakeCooldownCoef;

    Vector2 shakeVector;

    private void Awake()
    {
        EnemyEvents.scoreAdvancedEvent.AddListener(CameraShakeEventFunction);
    }

    private void CameraShakeEventFunction(int notUsed)
    {
        SetShakeVector(4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetCameraPosition = Vector3.Lerp(target.position, mousePosition, mouseLookControl) + new Vector3(0, 0, -10);
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, dampCoef * Time.fixedDeltaTime * 60);

        UpdateShakeVector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetShakeVector(10);
        }
    }

    public void SetShakeVector(float coef)
    {
        shakeCoef = coef;
        shakeVector = new(Random.Range(-coef, coef), Random.Range(-coef, coef));
    }

    private void UpdateShakeVector()
    {
        shakeCoef *= shakeCooldownCoef;
        if (shakeCoef <= 0.01f)
        {
            shakeCoef = 0;
            return;
        }
        SetShakeVector(shakeCoef);
        transform.position += (Vector3)shakeVector;
    }
}
