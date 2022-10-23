using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public UIManager uiManager;

    public GameObject boxes;
    public GameObject dropUIForC;
    public GameObject sphere;
    public GameObject player;

    public Material sphereBaseMaterial;
    public Material sphereUncoveredMaterial;


    private void OnEnable()
    {
        boxes.SetActive(false);
        UIManager.OnInstrumentCategoryOpened += UIManager_OnInstrumentCategoryOpened;
        UIManager.OnExitButtonPressed += ExitApplication;
        UIManager.OnRestartButtonPressed += RestartApplication;
        Box.OnSphereDroppedInBox += Box_OnSphereDroppedInBox;
    }

    private void OnDisable()
    {
        UIManager.OnInstrumentCategoryOpened -= UIManager_OnInstrumentCategoryOpened;
        UIManager.OnExitButtonPressed -= ExitApplication;
        UIManager.OnRestartButtonPressed -= RestartApplication;
        Box.OnSphereDroppedInBox -= Box_OnSphereDroppedInBox;
    }

    private void ResetToCheckpoint()
    {
        sphere.GetComponent<MeshRenderer>().material = sphereBaseMaterial;

        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = Quaternion.identity;

        sphere.transform.position = new Vector3(0, 0.25f, 6);
        sphere.transform.rotation = Quaternion.identity;

        dropUIForC.SetActive(false);
        boxes.SetActive(false);
    }

    private void UncoverInstrumentInSphere()
    {

        sphere.GetComponent<MeshRenderer>().material = sphereUncoveredMaterial;
        uiManager.ToggleMainUI(false);
        boxes.SetActive(true);
    }

    private void UIManager_OnInstrumentCategoryOpened()
    {
        StartCoroutine(AwaitAndExecute(2.0f, () => UncoverInstrumentInSphere()));
    }

    private void Box_OnSphereDroppedInBox(bool isReset)
    {
        if (isReset)
        {
            StartCoroutine(AwaitAndExecute(2.0f, () => ResetToCheckpoint()));
        }
        else
        {
            StartCoroutine(AwaitAndExecute(3.0f, () => uiManager.ShowEndPanel()));
        }
    }

    private IEnumerator AwaitAndExecute(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    private void ExitApplication()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void RestartApplication()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
