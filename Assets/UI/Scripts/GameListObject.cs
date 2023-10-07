using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameListObject : MonoBehaviour
{
    [SerializeField] Transform Panel;
    [SerializeField] GameObject DefaultButtonTemplate;

    [SerializeField] [Range(0, 10)] int ExtraItemsInPanelCount = 0;

    [SerializeField] Vector2 TotalButtonCount = new Vector2(5, 2);

    // https://forum.unity.com/threads/display-a-list-class-with-a-custom-editor-script.227847/
    [System.Serializable] public class ButtonInfo
    {
        public string Name;
        public Sprite Icon;
        public UnityEngine.Events.UnityAction OnClick;
    }
    [SerializeField] public List<ButtonInfo> Buttons = new List<ButtonInfo>(1);
    void AddNew()
    {
        //Add a new index position to the end of our list
        Buttons.Add(new ButtonInfo());
    }
    void Remove(int index)
    {
        //Remove an index position from our list at a point in our list array
        Debug.Log(index);
        Debug.Log(AddedButtons[index]);
        Buttons.RemoveAt(index);
        AddedButtons.RemoveAt(index);
    }
    void Edit()
    {
        Debug.Log("modified");
    }

    public List<ButtonInfo> AddedButtons;

    RectTransform PanelRect;
    Vector2 PanelSize;


    void Start()
    {
        PanelRect = Panel.GetComponent<RectTransform>();
        PanelSize = PanelRect.sizeDelta;
    }

    [ExecuteAlways]
    private void Update()
    {
        if (Buttons.Count > AddedButtons.Count)
        {
            foreach (ButtonInfo unaddedButton in Buttons)
            {
                if (!AddedButtons.Contains(unaddedButton))
                {
                    CreateButton(unaddedButton);
                    AddedButtons.Add(unaddedButton);
                }
            }
        }
        if (Buttons.Count < AddedButtons.Count)
        {
            foreach (ButtonInfo extraButton in AddedButtons)
            {
                if (!Buttons.Contains(extraButton))
                {
                    RemoveButton(extraButton);
                    AddedButtons.Remove(extraButton);
                }
            }
        }
    }

    public void AddButton(string Name, Sprite Icon, UnityEngine.Events.UnityAction OnClick)
    {
        ButtonInfo StoreData = new ButtonInfo();
        StoreData.Name = Name;
        StoreData.Icon = Icon;
        StoreData.OnClick = OnClick;
        Buttons.Add(StoreData);
    }

    public void RemoveButton(ButtonInfo _ButtonInfo)
    {
        Transform ButtonToDelete = Panel.GetChild(AddedButtons.IndexOf(_ButtonInfo) + ExtraItemsInPanelCount);

        DestroyImmediate(ButtonToDelete.gameObject);
    }

    public void CreateButton(ButtonInfo _ButtonInfo)
    {
        if (AddedButtons.Count+1 > TotalButtonCount.x*TotalButtonCount.y)
        {
            Debug.LogError("TOO MANY BUTTONS");
            return;
        }

        GameObject NewButton = (GameObject)Instantiate(DefaultButtonTemplate);
        
        NewButton.name = _ButtonInfo.Name;
        NewButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = _ButtonInfo.Name;

        NewButton.GetComponent<Image>().sprite = _ButtonInfo.Icon;

        NewButton.GetComponent<Button>().onClick.AddListener(_ButtonInfo.OnClick);

        NewButton.transform.SetParent(Panel, false);
        NewButton.transform.localPosition = CalculateButtonPosition(NewButton.transform.localScale);
    }

    Vector3 CalculateButtonPosition(Vector2 ButtonSize)
    {
        Vector2 FirstPosition = (PanelSize / 2) - (ButtonSize / 2);
        FirstPosition.x = -FirstPosition.x;

        Vector2 InbetweenButtonPadding = ( PanelSize - ( ButtonSize * TotalButtonCount + Vector2.one ) ) / TotalButtonCount;
        Vector2 FullButtonPadding = ButtonSize + InbetweenButtonPadding;

        Vector2 CalculatedPosition = FirstPosition;
        CalculatedPosition.x += FullButtonPadding.x * (AddedButtons.Count % TotalButtonCount.x); // x calc
        float MultiplicationAmount = (int)(AddedButtons.Count / TotalButtonCount.x); // y calc
        float NewYPos = FullButtonPadding.y * MultiplicationAmount;
        CalculatedPosition.y -= NewYPos;

        CalculatedPosition.x += FullButtonPadding.x / 2;
        // CalculatedPosition.x -= FullButtonPadding.x / 4;

        return CalculatedPosition;
    }
}
