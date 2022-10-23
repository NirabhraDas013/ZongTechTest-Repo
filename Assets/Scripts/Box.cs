using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    public delegate void OnSphereDrop(bool isReset);
    public static event OnSphereDrop OnSphereDroppedInBox;

    public GameObject dropUI;
    public TMP_Text dropText;
    public GameObject particleEffects;
    public string dropString;

    public void SphereDroppedInBox(string dropString, GameObject objectToActivate = null)
    {
        dropUI.SetActive(true);
        dropText.text = dropString;

        if(gameObject.CompareTag("C"))
        {
            OnSphereDroppedInBox?.Invoke(true);
        }
        else
        {
            OnSphereDroppedInBox?.Invoke(false);
            objectToActivate.SetActive(true);
        }

        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Sphere"))
            return;

        SphereDroppedInBox(dropString, particleEffects);
    }
}
