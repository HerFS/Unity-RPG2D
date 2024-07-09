using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public int[] npcId;
    GameManager gameManager;
    public bool[] isDropItem;

    public Sprite[] itemSprite;
    public string[] itemDescription;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GenerateData();
    }

    private void Update()
    {
        Debug.Log(npcId[0] + "," + npcId[1] + "," + gameManager.isAction + "," + gameManager.talkIndex);
        npcId[0] = GameObject.FindWithTag("NPC1").GetComponent<ObjData>().npcId;
        if (npcId[0] == 4 && !isDropItem[0] && gameManager.isAction && gameManager.talkIndex == 6)
        {
            Debug.Log("dropItem");
            isDropItem[0] = true;
            GameObject itemToDrop = new GameObject("Club");
            Item newItem = itemToDrop.AddComponent<Item>();
            newItem.quantity = 1;
            newItem.name = "Club";
            newItem.itemName = "Club";
            newItem.sprite = itemSprite[0];
            newItem.itemDescription = itemDescription[0];
            newItem.itemType = ItemType.weapon;

            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = itemSprite[0];
            sr.sortingOrder = 5;
            sr.sortingLayerName = "Item";

            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0f, -0.4f, 0);
            itemToDrop.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        npcId[1] = GameObject.FindWithTag("NPC2").GetComponent<ObjData>().npcId;
        if (npcId[1] == 3 && !isDropItem[1] && gameManager.isAction && gameManager.talkIndex == 10)
        {
            Debug.Log("dropItem");
            isDropItem[1] = true;
            GameObject itemToDrop = new GameObject("Blueberry");
            Item newItem = itemToDrop.AddComponent<Item>();
            newItem.quantity = 1;
            newItem.name = "Blueberry";
            newItem.itemName = "Blueberry";
            newItem.sprite = itemSprite[1];
            newItem.itemDescription = itemDescription[1];
            newItem.itemType = ItemType.consumable;

            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = itemSprite[1];
            sr.sortingOrder = 5;
            sr.sortingLayerName = "Item";

            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0f, -0.4f, 0);
            itemToDrop.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    void GenerateData()
    {
        talkData.Add(1, new string[] { "안녕! ", "지금 동물들을 잡으러가는 거니?", "무기는 있고?" });

        talkData.Add(2, new string[] { "안녕, 난 이 동물원의 대표일세. 허허허...",
            "알다시피 우리 동물원의 동물들이 무슨 이유인지는 몰라도 다 탈출해 버려서 남은 동물들이 없단다.",
            "너가 우리 동물원의 동물들을 다시 잡아서 와줄 수 있겠소?",
            "고맙네 이 은혜는 정말 잊지 않을것이오!",
            "마을 안의 사람들에게 물어보면 우리 동물원에 대해 친절하게 설명해줄걸세!",
            "하지만 잘 알려주지 않는 직원들도 있으니 잘 물어보게",
            "그럼 당신만 믿고 기다리고 있겠네."});

        talkData.Add(3, new string[] { "안녕 나는 이 동물원의 마스코트를 맡고 있는 포세이돈이야",
            "내가 너의 정보에 대해 알려줄게!",
            "왼쪽 위에 창 보이지? 그게 너의 스탯이야! 초록색 바는 너의 HP, 노란색은 너의 경험치 표시를 해주지",
            "혹시나 HP가 모두 소진되면 정신을 잃게 될테니까 조심하라구",
            "HP를 회복하고 싶다고? 아니 뭐 하나하나 다 알려줘야되나?",
            "난 착하니까 설명해주지. 너가 동물들을 잡게되면 동물들이 가져간 음식들을 흘릴거야.",
            "그 음식 아이템들을 더블 클릭하면 HP가 회복될거야.",
            "그러니 잘 기억해둬야돼!",
            "그리고 혹시나 음식이나 다른 아이템들을 버리고 싶으면 아이템 오른쪽 클릭을 하면 될거야!",
            "내가 특별히 열매 한 개를 줄게. 그럼 동물들을 모두 잡아서 와줘~"});

        talkData.Add(4, new string[] { "너가 이번에 동물들을 잡으로 온 사육사구나? 만나서 반가워",
            "근데 동물들은 뭘로 잡으려고..?",
            "너가 힘이 쎄다면 괜찮겠지만 나는 무기를 사용하는 것을 추천할게",
            "무기는 동물들을 잡으면 떨어뜨리기도 해!",
            "무기를 장착하는 방법은 E를 눌러 장비창에서 너가 착용하고 싶은 무기를 얻어서 더블 클릭하면 장착할 수 있게되지",
            "내가 몽둥이 하나를 줄게 급한대로 이거라도 써!",
            "그럼 이만! 행운을 빌게"});

        talkData.Add(5, new string[] { "드넓은 초원에 오니까 어때? 가슴이 뻥 뚫리지 않아?",
            "이런 곳이라면 우리 동물들을 찾기가 더 수월하겠어",
            "새 잡기는 비교적 수월하겠지만 사슴은 보기와는 다르게 강하니까 조심해",});

        talkData.Add(6, new string[] { "후... 여기 너무 덥지 않아?",
            "이렇게 더운 곳에서도 동물들이 산다는게 참 신기해..",
            "여기 사막엔 우리 동물원에서 탈출한 토끼, 여우 등 많은 동물들이 있을거야!",
            "토끼나 여우같은 동물들은 작아서 손쉽게 잡을 수 있지만",
            "뱀이나 높은 레벨의 동물들은 잡기가 쉽지 않을거야",
            "그러니 조심해! 시간이 벌써 없네 얼른 가서 잡아와줘"});

        talkData.Add(7, new string[] { "다른 지형에 비해 여기는 으스스해...",
            "뭔가 무서운 동물들이 숨어 있을 것 같은 곳이야.. 그니까 조심해야 돼",
            "얼른 잡고 서둘러 여길 벗어나!"});

        talkData.Add(8, new string[] { "안녕 나는 잡상인이야 내가 신기한 사실을 알아왔는데 들어볼래?",
            "얼마 전에 누구한테 들었는데 어떠한 동물을 잡으면 전설의 검이 나온다고 그러더라고",
            "그 동물의 힌트를 알려줄까?",
            "그 동물의 힌트는 크기가 가장 크다고 하더라고."});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

}
