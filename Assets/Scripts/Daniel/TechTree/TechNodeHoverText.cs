using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechNodeHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UIManager uiManager;
    TechNode techNodeScript;

    private void Start()
    {
        techNodeScript = transform.parent.GetComponent<TechNode>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OpenPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exitted");
        uiManager.CloseHoverDisplayPanel();
    }

    public void OpenPanel()
    {
        //Debug.Log(techNodeScript.GetHoverData().objectName);
        uiManager.OpenHoverDisplayPanel(techNodeScript.GetHoverData());
    }
}