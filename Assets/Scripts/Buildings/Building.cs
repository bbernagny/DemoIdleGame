using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    public ObjectData objectData;
    private float _timer;
    public Slider slider;
    public TextMeshProUGUI floatingText;

    private void Update()
    {
        if (objectData != null)
        {
            objectData.GenerateGoldAndGem(objectData.productionTime, ref _timer);
            slider.maxValue = objectData.productionTime;
            slider.value = _timer;

            if (slider.value >= objectData.productionTime - .05f)
            {
                Debug.Log("1");  
                StartCoroutine(FloatingTextActivity());
            }
        }
    }

    private IEnumerator FloatingTextActivity()
    {
        floatingText.transform.gameObject.SetActive(true);
        Debug.Log("A");
        floatingText.SetText("+" + (objectData.generatedGold + objectData.generatedGem).ToString());
        yield return new WaitForSeconds(1f);
        Debug.Log("B");
        floatingText.transform.gameObject.SetActive(false);
    }
}