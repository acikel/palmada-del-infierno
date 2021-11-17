
using System.Collections;
using UnityEngine;

public class BossMelee : State
{
    private float attackInterval = 1f;
    private float attackDamage = 1f;
    private float attackRange = 1.5f;
    private float attackDelay = 0f;

    private GameObject bossFist;
    private Vector3 bossFistPosition;
    
    public override void OnStart()
    {
        Debug.Log("Boss Melee");

        bossFist = GameObject.transform.Find("BossFist").gameObject;
        bossFistPosition = bossFist.transform.position;
        
        StartCoroutine(Melee());
    }
    
    private IEnumerator Melee()
    {
        bossFist.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        Vector3 goalPos = bossFistPosition + (Vector3.down * 3);

        while (Vector3.Distance(bossFist.transform.position, goalPos) > 0.1f)
        {
            bossFist.transform.position =
                Vector3.Lerp(bossFist.transform.position, goalPos, Time.deltaTime * 20);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        bossFist.SetActive(false);
        bossFist.transform.position = bossFistPosition;
        
        SetState(new BossDecision());
    }
}
