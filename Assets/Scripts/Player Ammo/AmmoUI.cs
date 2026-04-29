using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI ammoText;

    public GameObject container;

    public Transform queueContainer;
    public GameObject queueIconPrefab;

    public Sprite fireSprite;
    public Sprite iceSprite;
    public Sprite poisonSprite;

    private List<GameObject> queueIcons = new List<GameObject>();

    public void UpdateUI(AmmoData data, int currentAmmo)
    {
        container.SetActive(true);
        icon.sprite = GetSprite(data.ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    public void SetDefault()
    {
        container.SetActive(false);
        ClearQueue();
    }

    public void UpdateQueue(IEnumerable<AmmoQueueItem> queue)
    {
        ClearQueue();

        foreach (var item in queue)
        {
            GameObject obj = Instantiate(queueIconPrefab, queueContainer);
            obj.GetComponent<Image>().sprite = GetSprite(item.data.ammoType);
            queueIcons.Add(obj);
        }
    }

    void ClearQueue()
    {
        foreach (var obj in queueIcons)
        {
            Destroy(obj);
        }

        queueIcons.Clear();
    }

    Sprite GetSprite(AmmoType type)
    {
        switch (type)
        {
            case AmmoType.Fire: return fireSprite;
            case AmmoType.Ice: return iceSprite;
            case AmmoType.Poison: return poisonSprite;
            default: return null;
        }
    }
}