using System.Collections;
using UnityEngine;

public class RockController : MonoBehaviour
{
    Coroutine activeCoroutine;
    public Material[] fadeMaterials;
    public Material[] opaqueMaterials;
    /*
    private void Start()
    {
        // avoid clipping
        GetComponent<MeshRenderer>().materials[0].renderQueue = 3001;
    }
    */


    private void OnEnable()
    {
        activeCoroutine = null;
        // Material[] materials = GetComponent<MeshRenderer>().materials;
        GetComponent<MeshRenderer>().materials = opaqueMaterials;
        /*
        for (int i = 0; i < materials.Length; i++)
        {
            Color curColor = materials[i].color;
            curColor.a = 1;
            materials[i].color = curColor;
            MaterialExtensions.ToOpaqueMode(materials[i]);
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && activeCoroutine == null)
        {
            activeCoroutine = StartCoroutine(MoveAway());
        }
    }

    IEnumerator MoveAway()
    {
        float timer = 0;
        float finishTime = 5;
        GetComponent<MeshRenderer>().materials = fadeMaterials;
        Material[] materials = GetComponent<MeshRenderer>().materials;
        /*(
        foreach (Material material in materials)
        {            
            // fade
            MaterialExtensions.ToFadeMode(material);
        }
        */
        Color curColor;
        while (timer < finishTime)
        {
            for (int i=0; i<materials.Length; i++)
            {
                curColor = materials[i].color;
                curColor.a = Mathf.Lerp(1, 0, Mathf.Pow(timer, 2) / Mathf.Pow(finishTime, 2));
                materials[i].color = curColor; 
            }
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlatformSpawner.instance.platformSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
