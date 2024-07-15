using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TeamManager;

public class ResultManager : MonoBehaviour
{
    TeamManager teamManager;
    List<Image> leaderImages;
    List<TextMeshProUGUI> leaderNames;
    public List<List<Image>> teamMemberImages;
    public List<List<TMP_Text>> teamMemberNames;
    // Start is called before the first frame update
    void Start()
    {
        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();
    }

    // UI 초기화
    public void InitializeResultUI()
    {
        leaderImages = new List<Image>();
        leaderNames = new List<TextMeshProUGUI>();
        for (int i = 0; i < 10; i++)
        {
            leaderImages.Add(GameObject.Find("LeaderImage" + i).GetComponent<Image>());
            leaderNames.Add(GameObject.Find("LeaderName" + i).GetComponent<TextMeshProUGUI>());
        }

        for (int i = 0; i < 10; i++)
        {
            leaderImages[i].sprite = Resources.Load<Sprite>("Images/TeamLeader/" + teamManager.teams[i].leader[0].name);
            leaderNames[i].text = teamManager.teams[i].leader[0].name;
        }

        teamMemberImages = new List<List<Image>>();
        for (int i = 0; i < teamManager.teams.Count; i++)
        {
            teamMemberImages.Add(new List<Image>());
            for (int j = 0; j < 2; j++)
            {
                teamMemberImages[i].Add(GameObject.Find("ResultTeam" + i + "Member" + j + "Image").GetComponent<Image>());
            }
        }

        teamMemberNames = new List<List<TMP_Text>>();
        for (int i = 0; i < teamManager.teams.Count; i++)
        {
            teamMemberNames.Add(new List<TMP_Text>());
            for (int j = 0; j < 2; j++)
            {
                teamMemberNames[i].Add(GameObject.Find("ResultTeam" + i + "Member" + j + "Name").GetComponent<TMP_Text>());
            }
        }

        UpdateResultUI();
    }

    public void UpdateResultUI()
    {
        for (int i = 0; i < teamManager.teams.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (teamManager.teams[i].members.Count > j)
                {
                    teamMemberImages[i][j].sprite = Resources.Load<Sprite>("Images/TeamMember/" + teamManager.teams[i].members[j].name);
                    teamMemberNames[i][j].text = teamManager.teams[i].members[j].name;
                }
                else
                {
                    Debug.Log("팀원을 불러오는데에 문제가 발생했습니다.");
                    teamMemberImages[i][j].sprite = null;
                    teamMemberNames[i][j].text = "";
                }
            }
        }
    }
}
