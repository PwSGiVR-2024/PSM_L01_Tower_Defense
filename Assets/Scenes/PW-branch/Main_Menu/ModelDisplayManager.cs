using UnityEngine;

public class ModelDisplayManager : MonoBehaviour
{
    public Transform modelParent; // miejsce gdzie b�d� instancjowane modele
    public GameObject virusPrefab;
    public GameObject trojanPrefab;
    public GameObject ratPrefab;

    private GameObject currentModel;

    public void ShowVirus()
    {
        ShowModel(virusPrefab);
    }

    public void ShowTrojan()
    {
        ShowModel(trojanPrefab);
    }

    public void ShowRat()
    {
        ShowModel(ratPrefab);
    }

    void ShowModel(GameObject prefab)
    {
        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(prefab, modelParent.position, modelParent.rotation, modelParent);
        currentModel.transform.localPosition = Vector3.zero;  // reset lokalnej pozycji, �eby dobrze si� pokaza�o
        currentModel.transform.localRotation = Quaternion.identity;
        currentModel.transform.localScale = Vector3.one;      // dopasuj skal� je�li trzeba
    }
}
