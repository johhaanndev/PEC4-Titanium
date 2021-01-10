using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public GameObject movingPoint;
    private bool startShake = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartShaking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartShaking()
    {
        float ran;

        while (!startShake)
        {
            ran = Random.Range(0f, 2f);
            yield return new WaitForSeconds(1f);
            CinemachineShake.Instance.ShakeCamera(ran, 1f);
        }
        
        CinemachineShake.Instance.ShakeCamera(0f, 0.1f);
        yield return new WaitForSeconds(8.30f);
        CinemachineShake.Instance.ShakeCamera(2f, 2f);
        yield return new WaitForSeconds(4.35f);
        CinemachineShake.Instance.ShakeCamera(2f, 2f);
        yield return new WaitForSeconds(4.30f);
        for (int i = 0; i < 8; i++)
        {
            CinemachineShake.Instance.ShakeCamera(2f, 0.15f);
            yield return new WaitForSeconds(1.08f);
        }
        yield return new WaitForSeconds(1.06f);
        CinemachineShake.Instance.ShakeCamera(1f, 1.10f);
        yield return new WaitForSeconds(1.06f);
        CinemachineShake.Instance.ShakeCamera(2f, 1.10f);
        yield return new WaitForSeconds(1.06f);
        CinemachineShake.Instance.ShakeCamera(3f, 1.10f);
        yield return new WaitForSeconds(1.06f);
        CinemachineShake.Instance.ShakeCamera(4f, 1.10f);
        yield return new WaitForSeconds(1.06f);
        CinemachineShake.Instance.ShakeCamera(5f, 1.10f);

        yield return new WaitForSeconds(3.3f);
        
        for (int i = 1; i< 64; i++)
        {
            if (i % 32 == 0 || i == 1)
            {
                CinemachineShake.Instance.ShakeCamera(8f, 0.2f);
                yield return new WaitForSeconds(0.545f);
            }

            CinemachineShake.Instance.ShakeCamera(3f, 0.15f);
            yield return new WaitForSeconds(0.545f);
        }

    }

    public void SetStartShake(bool startShake)
    {
        this.startShake = startShake;
    }
}
