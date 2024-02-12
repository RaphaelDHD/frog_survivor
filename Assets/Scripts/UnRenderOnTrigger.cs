using UnityEngine;

public class UnRenderOnTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground"){
            Renderer[] childRenderers = other.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer childRenderer in childRenderers)
            {
            childRenderer.enabled = true;
            }       
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground"){
            Renderer[] childRenderers = other.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer childRenderer in childRenderers)
            {
            childRenderer.enabled = false;
            }       
        }
    }
}
