using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour {
    private static WindowGraph instance;

    [SerializeField] private Sprite dotSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;
    private int pageIndex;
    private IGraphVisual lineGraphVisual;
    private GameObject tooltipGameObject;

    private List<int> valueList;
    private IGraphVisual graphVisual;
    private int maxVisibleValueAmount;
    private Func<int, string> getAxisLabelX;
    private Func<float, string> getAxisLabelY;

    private void Awake() {
        instance = this;
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("LabelX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("DashX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("DashY").GetComponent<RectTransform>();
        tooltipGameObject = graphContainer.Find("Tooltip").gameObject;

        gameObjectList = new List<GameObject>();
        
        lineGraphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.green, new Color(1, 1, 1, .5f));
        IGraphVisual barChartVisual = new BarChartVisual(graphContainer, Color.white, .8f);
        
        transform.Find("BarChartBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGraphVisual(barChartVisual);
        };
        transform.Find("LineGraphBtn").GetComponent<Button_UI>().ClickFunc = () => {
            ((LineGraphVisual) lineGraphVisual).ClearLastDot(); 
            SetGraphVisual(lineGraphVisual);
        };

        transform.Find("PreviousValuesBtn").GetComponent<Button_UI>().ClickFunc = () => {
            ((LineGraphVisual) lineGraphVisual).ClearLastDot();
            LoadPreviousValues();;
        };
        transform.Find("NextValuesBtn").GetComponent<Button_UI>().ClickFunc = () => {
            ((LineGraphVisual) lineGraphVisual).ClearLastDot();
            LoadNextValues();
        };
        
        HideTooltip();
        List<int> valueList = new List<int> { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33, 50, 30, 60, 50, 40, 20, 5, 20, 10, 50, 30, 20, 11,
        5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33, 50, 30, 60, 50, 40, 20, 5, 20, 10, 50, 30, 20, 11 };
        ShowGraph(valueList, barChartVisual, -1, i => "" + (i + 1), f => "" + Mathf.RoundToInt(f));
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ((LineGraphVisual) lineGraphVisual).ClearLastDot(); 
            LoadNextValues();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ((LineGraphVisual) lineGraphVisual).ClearLastDot();
            LoadPreviousValues();
        }
    }
    
    private static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition) {
        instance.ShowTooltip(tooltipText, anchoredPosition);
    }

    private void ShowTooltip(string tooltipText, Vector2 anchoredPosition) {
        tooltipGameObject.SetActive(true);

        tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition - new Vector2(30, 30);

        Text tooltipUIText = tooltipGameObject.transform.Find("Text").GetComponent<Text>();
        tooltipUIText.text = tooltipText;

        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(
            tooltipUIText.preferredWidth + textPaddingSize * 2f, 
            tooltipUIText.preferredHeight + textPaddingSize * 2f
        );

        tooltipGameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = backgroundSize;
        tooltipGameObject.transform.SetAsLastSibling();
    }

    private static void HideTooltip_Static() {
        instance.HideTooltip();
    }

    private void HideTooltip() {
        tooltipGameObject.SetActive(false);
    }

    private void LoadPreviousValues() {
        ShowGraph(valueList, graphVisual, maxVisibleValueAmount, getAxisLabelX, getAxisLabelY, --pageIndex);
    }

    private void LoadNextValues() {
        ShowGraph(valueList, graphVisual, maxVisibleValueAmount, getAxisLabelX, getAxisLabelY, ++pageIndex);
    }

    private void SetGetAxisLabelX(Func<int, string> getAxisLabelX) {
        ShowGraph(valueList, graphVisual, maxVisibleValueAmount, getAxisLabelX, getAxisLabelY);
    }

    private void SetGetAxisLabelY(Func<float, string> getAxisLabelY) {
        ShowGraph(valueList, graphVisual, maxVisibleValueAmount, getAxisLabelX, getAxisLabelY);
    }

    private void SetGraphVisual(IGraphVisual graphVisual) {
        ShowGraph(valueList, graphVisual, maxVisibleValueAmount, getAxisLabelX, getAxisLabelY, pageIndex);
    }

    private void ShowGraph(List<int> valueList, IGraphVisual graphVisual, int maxVisibleValueAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null, int initialValue = 0) {
        this.valueList = valueList;
        this.graphVisual = graphVisual;
        this.getAxisLabelX = getAxisLabelX;
        this.getAxisLabelY = getAxisLabelY;

        if (initialValue < 0) {
            pageIndex++;
            initialValue = 0;
        }
        if (initialValue + 15 >= valueList.Count) {
            pageIndex--;
        }

        maxVisibleValueAmount = 15;
        if (maxVisibleValueAmount > valueList.Count) {
            maxVisibleValueAmount = valueList.Count;
        }

        this.maxVisibleValueAmount = maxVisibleValueAmount;

        getAxisLabelX ??= delegate(int i) { return i.ToString(); };
        getAxisLabelY ??= delegate(float f) { return Mathf.RoundToInt(f).ToString(); };

        foreach (GameObject gameObject in gameObjectList) {
            Destroy(gameObject);
        }
        gameObjectList.Clear();
        
        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        float yMaximum = 100f;
        float yMinimum = 0f;

        float xSize = graphWidth / (maxVisibleValueAmount + 1);

        int xIndex = 0;
        for (int i = initialValue; i < Mathf.Min(initialValue + 15, valueList.Count); i++) {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;

            string tooltipText = getAxisLabelY(valueList[i]);
            gameObjectList.AddRange(graphVisual.AddGraphVisual(new Vector2(xPosition, yPosition), xSize, tooltipText));

            RectTransform labelX = Instantiate(labelTemplateX, graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -10f);
            labelX.GetComponent<TextMeshProUGUI>().text = getAxisLabelX(i);
            gameObjectList.Add(labelX.gameObject);
            
            RectTransform dashX = Instantiate(dashTemplateX, graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.gameObject.transform.SetSiblingIndex(1);
            dashX.anchoredPosition = new Vector2(xPosition, -3f);
            gameObjectList.Add(dashX.gameObject);

            xIndex++;
        }

        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++) {
            RectTransform labelY = Instantiate(labelTemplateY, graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-18f, normalizedValue * graphHeight);
            labelY.GetComponent<TextMeshProUGUI>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            gameObjectList.Add(labelY.gameObject);

            RectTransform dashY = Instantiate(dashTemplateY, graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.gameObject.transform.SetSiblingIndex(1);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }
    }

    private interface IGraphVisual {
        List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
    }

    private class BarChartVisual : IGraphVisual {
        private RectTransform graphContainer;
        private Color barColor;
        private float barWidthMultiplier;

        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier) {
            this.graphContainer = graphContainer;
            this.barColor = barColor;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth, string tooltipText) {
            GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth);
            Button_UI barButtonUI = barGameObject.AddComponent<Button_UI>();

            barButtonUI.MouseOverOnceFunc += () => {
                ShowTooltip_Static(tooltipText, graphPosition - new Vector2(415, 170));
            };

            barButtonUI.MouseOutOnceFunc += () => {
                HideTooltip_Static();
            };
            return new List<GameObject>() { barGameObject };
        }

        private GameObject CreateBar(Vector2 graphPosition, float barWidth) {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = barColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPosition.y);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);
            return gameObject;
        }
    }

    private class LineGraphVisual : IGraphVisual {
        private RectTransform graphContainer;
        private Sprite dotSprite;
        private GameObject lastDotGameObject;
        private Color dotColor;
        private Color dotConnectionColor;

        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor) {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            this.dotColor = dotColor;
            this.dotConnectionColor = dotConnectionColor;
            lastDotGameObject = null;
        }

        public List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth, string tooltipText) {
            List<GameObject> gameObjectList = new List<GameObject>();
            GameObject dotGameObject = CreateDot(graphPosition);
            Button_UI dotButtonUI = dotGameObject.AddComponent<Button_UI>();
            
            dotButtonUI.MouseOverOnceFunc += () => {
                ShowTooltip_Static(tooltipText, graphPosition - new Vector2(440, 180));
            };
            
            dotButtonUI.MouseOutOnceFunc += () => {
                HideTooltip_Static();
            };

            gameObjectList.Add(dotGameObject);
            if (lastDotGameObject != null) {
                GameObject dotConnectionGameObject = CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastDotGameObject = dotGameObject;
            return gameObjectList;
        }

        private GameObject CreateDot(Vector2 anchoredPosition) {
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = dotSprite;
            gameObject.GetComponent<Image>().color = dotColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return gameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
            GameObject gameObject = new GameObject("dotConnection", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = dotConnectionColor;
            gameObject.GetComponent<Image>().raycastTarget = false;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 3f);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
            return gameObject;
        }

        public void ClearLastDot() {
            lastDotGameObject = null;
        }
    }

}
