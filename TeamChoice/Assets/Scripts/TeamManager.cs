using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamManager : MonoBehaviour
{
    // 팀장 정보를 담는 클래스
    [System.Serializable]
    public class TeamLeader
    {
        public string name;
        public int order;

        public TeamLeader(string name, int order)
        {
            this.name = name;
            this.order = order;
        }
    }
    // 팀원 정보를 담는 클래스
    [System.Serializable]
    public class TeamMember
    {
        public string name;
        public int order;
        public string imageName;

        public TeamMember(string name, int order, string imageName)
        {
            this.name = name;
            this.order = order;
            this.imageName = imageName;
        }
    }
    // 팀 정보를 담는 클래스
    [System.Serializable]
    public class Team
    {
        public List<TeamLeader> leader;
        public List<TeamMember> members;

        public Team(List<TeamLeader> leader, List<TeamMember> members)
        {
            this.leader = leader;
            this.members = members;
        }
    }

    public List<TeamLeader> teamLeaders;
    public List<TeamMember> teamMembers;
    public List<TeamMember> kickMembers;
    public List<TeamMember> waitingTeamMembers;
    public List<Image> leaderImages;
    public List<TMP_Text> leaderNames;
    public List<Team> teams;
    public List<List<Image>> teamMemberImages;
    public List<List<TMP_Text>> teamMemberNames;
    public GameObject kickPanel;
    public List<Image> kickMemberImages;
    public List<TMP_Text> kickMemberNames;
    public GameObject waitingList;
    public GameObject pickWaitngMemberPanel;
    public GameObject showResultPanel;
    List<Team> teamsWithLessThanTwoMembers;
    List<Team> teamsWithZeroMembers;
    List<Team> teamsWithOneMember;
    public ChoiceManager choiceManager;
    public ResultManager resultManager;

    private const int MaxMembersPerTeam = 2;

    public void Start()
    {
        resultManager = GameObject.Find("ResultManager").GetComponent<ResultManager>();
        choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        // 팀장, 팀원, 팀 정보 초기화
        InitializeTeams();
        InitializeUI();
        LoadFirstMember();
    }

    // 팀장, 팀원, 팀 정보 초기화
    private void InitializeTeams()
    {
        teamLeaders = new List<TeamLeader>
        {
            new TeamLeader("팀장A", 0),
            new TeamLeader("팀장B", 1),
            new TeamLeader("팀장C", 2),
            new TeamLeader("팀장D", 3),
            new TeamLeader("팀장E", 4),
            new TeamLeader("팀장F", 5),
            new TeamLeader("팀장G", 6),
            new TeamLeader("팀장H", 7),
            new TeamLeader("팀장I", 8),
            new TeamLeader("팀장J", 9)
        };

        teamMembers = new List<TeamMember>
        {
        new TeamMember("팀원1", 0, "팀원1"),
        new TeamMember("팀원2", 1, "팀원2"),
        new TeamMember("팀원3", 2, "팀원3"),
        new TeamMember("팀원4", 3, "팀원4"),
        new TeamMember("팀원5", 4, "팀원5"),
        new TeamMember("팀원6", 5, "팀원6"),
        new TeamMember("팀원7", 6, "팀원7"),
        new TeamMember("팀원8", 7, "팀원8"),
        new TeamMember("팀원9", 8, "팀원9"),
        new TeamMember("팀원10", 9, "팀원10"),
        new TeamMember("팀원11", 10, "팀원11"),
        new TeamMember("팀원12", 11, "팀원12"),
        new TeamMember("팀원13", 12, "팀원13"),
        new TeamMember("팀원14", 13, "팀원14"),
        new TeamMember("팀원15", 14, "팀원15"),
        new TeamMember("팀원16", 15, "팀원16"),
        new TeamMember("팀원17", 16, "팀원17"),
        new TeamMember("팀원18", 17, "팀원18"),
        new TeamMember("팀원19", 18, "팀원19"),
        new TeamMember("팀원20", 19, "팀원20")
        };

        teams = new List<Team>();
        for (int i = 0; i < teamLeaders.Count; i++)
        {
            teams.Add(new Team(new List<TeamLeader> { teamLeaders[i] }, new List<TeamMember>()));
        }
    }

    // 팀장 이미지와 이름을 초기화
    private void InitializeUI()
    {
        for (int i = 0; i < leaderNames.Count && i < teamLeaders.Count; i++)
        {
            leaderNames[i].text = teamLeaders[i].name;
        }

        for (int i = 0; i < leaderImages.Count && i < teamLeaders.Count; i++)
        {
            leaderImages[i].sprite = Resources.Load<Sprite>("Images/TeamLeader/" + teamLeaders[i].name);
        }

        teamMemberImages = new List<List<Image>>();
        for (int i = 0; i < teams.Count; i++)
        {
            teamMemberImages.Add(new List<Image>());
            for (int j = 0; j < MaxMembersPerTeam; j++)
            {
                teamMemberImages[i].Add(GameObject.Find("Team" + i + "Member" + j + "Image").GetComponent<Image>());
            }
        }

        teamMemberNames = new List<List<TMP_Text>>();
        for (int i = 0; i < teams.Count; i++)
        {
            teamMemberNames.Add(new List<TMP_Text>());
            for (int j = 0; j < MaxMembersPerTeam; j++)
            {
                teamMemberNames[i].Add(GameObject.Find("Team" + i + "Member" + j + "Name").GetComponent<TMP_Text>());
            }
        }
    }

    public void LoadFirstMember()
    {
        // 첫번째 멤버를 불러오고 초기화
        choiceManager.currentTeamMember = teamMembers[0];
        Debug.Log(choiceManager.currentTeamMember.name);
        Debug.Log(choiceManager.currentTeamMember.order);
    }

    public void LoadNextMember(int index)
    {
        // 다음 멤버를 불러오고 초기화
        choiceManager.currentTeamMember = teamMembers[index];
        Debug.Log(choiceManager.currentTeamMember.name);
        Debug.Log(choiceManager.currentTeamMember.order);
    }

    public void AddMemberToTeam(int teamIndex)
    {
        // 팀에 멤버 추가
        Team team = teams.FirstOrDefault(t => t.leader[0].order == teamIndex);

        if (team != null && team.members.Count < MaxMembersPerTeam)
        {
            team.members.Add(choiceManager.currentTeamMember);
            UpdateTeamUI(teamIndex, team.members.Count - 1, choiceManager.currentTeamMember);
            if(choiceManager.currentMemberIndex == 19)
            {
                choiceManager.currentMemberIndex++;
            }
            choiceManager.LoadNextMember();
        }
        else if (team != null && team.members.Count == MaxMembersPerTeam)
        {
            choiceManager.currentTeamIndex = teamIndex;
            OpenKickPanel(teamIndex);
        }
    }

    // kickPanel을 활성화하고, 현재 선택한 팀의 팀원을 호출
    public void OpenKickPanel(int teamIndex)
    {
        kickPanel.SetActive(true);
        // 현재 선택한 팀 호출
        Team team = teams.FirstOrDefault(t => t.leader[0].order == teamIndex);
        // kickMemberNames에 teamindex에 해당하는 팀원 이름과 choiceManager의 currentTeamMember 이름을 넣어줌
        kickMemberNames[0].text = team.members[0].name;
        kickMemberNames[1].text = team.members[1].name;
        kickMemberNames[2].text = choiceManager.currentTeamMember.name;
        // kickMemberImages에 teamindex에 해당하는 팀원 이미지와 choiceManager의 currentTeamMember 이미지를 넣어줌
        kickMemberImages[0].sprite = Resources.Load<Sprite>("Images/TeamMember/" + team.members[0].imageName);
        kickMemberImages[1].sprite = Resources.Load<Sprite>("Images/TeamMember/" + team.members[1].imageName);
        kickMemberImages[2].sprite = Resources.Load<Sprite>("Images/TeamMember/" + choiceManager.currentTeamMember.imageName);
        // kickMembers에 teamindex에 해당하는 팀원과 chiocemanager의 currentTeamMember를 넣어줌
        kickMembers = new List<TeamMember> { team.members[0], team.members[1], choiceManager.currentTeamMember };
    }

    // kickmembers 리스트에 있는 지정한 멤버를 waitingMembers 리스트로 이동시키고 나머지 팀원은 기존에 있던 팀 리스트로 이동
    public void KickMemberFromTeam(int memberIndex)
    {
        // kickMembers 리스트에 있는 팀원을 teams 리스트에서 삭제
        Team team = teams.FirstOrDefault(t => t.leader[0].order == choiceManager.currentTeamIndex);
        team.members.Clear();
        // waitingTeamMembers 리스트에 kickmembers 리스트에서 memberIndex 차례에 해당하는 팀원을 추가
        waitingTeamMembers.Add(kickMembers[memberIndex]);
        UpdateWaitingListUI(kickMembers[memberIndex]);
        // kickMembers 리스트에서 memberIndex 차례에 해당하는 팀원을 삭제
        kickMembers.RemoveAt(memberIndex);
        // kickMembers 리스트 정렬
        kickMembers.Sort((a, b) => a.order.CompareTo(b.order));
        // kickMemers 리스트에 있는 팀원을 teams 리스트에 추가
        for (int i = 0; i < kickMembers.Count; i++)
        {
            team.members.Add(kickMembers[i]);
            UpdateTeamUI(choiceManager.currentTeamIndex, i, kickMembers[i]);
        }
        // kickMembers 리스트 초기화
        kickMembers.Clear();
        // kickPanel 비활성화
        kickPanel.SetActive(false);
        // 팀원 UI 업데이트
        for (int i = 0; i < team.members.Count; i++)
        {
            UpdateTeamUI(choiceManager.currentTeamIndex, i, team.members[i]);
        }
        if (choiceManager.currentMemberIndex == 19)
        {
            choiceManager.currentMemberIndex++;
        }
        choiceManager.LoadNextMember();
        // choiceManager의 currentTeamIndex 초기화
        choiceManager.currentTeamIndex = 10;
    }

    // 팀원 이미지와 이름을 업데이트
    private void UpdateTeamUI(int teamIndex, int memberIndex, TeamMember member)
    {
        string imagePath = "Images/TeamMember/" + member.imageName;
        Sprite memberSprite = Resources.Load<Sprite>(imagePath);
        if (memberSprite == null)
        {
            Debug.LogError("Failed to load sprite at path: " + imagePath);
            return;
        }
        teamMemberImages[teamIndex][memberIndex].sprite = memberSprite;
        teamMemberNames[teamIndex][memberIndex].text = member.name;
    }

    void UpdateWaitingListUI(TeamMember memeber)
    {
        // waitingTeamMembers 리스트에 있는 팀원을 prefabs로 생성하되, 이미 생성된 prefabs가 있다면 그대로 사용
        GameObject prefab = Resources.Load<GameObject>("Prefabs/WaitingMember");
        // teamMember는 waitingList의 자식으로 생성
        GameObject teamMember = Instantiate(prefab, waitingList.transform);
        // teamMember의 자식 오브젝트에서 이미지와 이름을 찾아서 설정
        Image image = teamMember.transform.Find("Image").GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Images/TeamMember/" + memeber.imageName);
        TMP_Text name = teamMember.transform.Find("Name").GetComponent<TMP_Text>();
        name.text = memeber.name;
    }

    // ChoiceManager의 currentTeamIndex가 19나 20일 때, members의 수가 2보다 적은 team을 모두 찾는다.
    public void FindTeamWithLessThanTwoMembers()
    {
        teamsWithLessThanTwoMembers = teams.Where(t => t.members.Count < MaxMembersPerTeam).ToList();
        // teamsWithLessThanTwoMembers 리스트를 members.Count가 적은 순으로 정렬
        teamsWithLessThanTwoMembers.Sort((a, b) => a.members.Count.CompareTo(b.members.Count));
        // members.count가 0인 팀끼리, 1인 팀끼리 구별하여 정렬
        teamsWithZeroMembers = teamsWithLessThanTwoMembers.Where(t => t.members.Count == 0).ToList();
        teamsWithOneMember = teamsWithLessThanTwoMembers.Where(t => t.members.Count == 1).ToList();
        // teamsWithZeroMembers의 순서를 무작위로 섞음
        teamsWithZeroMembers = teamsWithZeroMembers.OrderBy(t => Random.Range(0, 100)).ToList();
        // teamsWithOneMember의 순서를 무작위로 섞음
        teamsWithOneMember = teamsWithOneMember.OrderBy(t => Random.Range(0, 100)).ToList();
        pickWaitngMemberPanel.SetActive(true);

        if(teamsWithZeroMembers.Count > 0)
        {
            // teamsWithZeroMembers의 리스트를 하나의 TMP_Text에 전부 표시
            TMP_Text zeroMemberText = pickWaitngMemberPanel.transform.Find("Page0/TeamWithZeroMembers").GetComponent<TMP_Text>();
            zeroMemberText.text = "팀원이 0명인 팀장은 ";
            for (int i = 0; i < teamsWithZeroMembers.Count; i++)
            {
                zeroMemberText.text += teamsWithZeroMembers[i].leader[0].name;
                if (i < teamsWithZeroMembers.Count - 1)
                {
                    zeroMemberText.text += ", ";
                }
            }
            zeroMemberText.text += " 입니다.";

        }
        else
        {
            TMP_Text zeroMemberText = pickWaitngMemberPanel.transform.Find("Page0/TeamWithZeroMembers").GetComponent<TMP_Text>();
            zeroMemberText.text = "팀원이 0명인 팀장은 없습니다.";
        }

        if (teamsWithOneMember.Count > 0)
        {
            // teamsWithOneMember의 리스트를 하나의 TMP_Text에 전부 표시
            TMP_Text oneMemberText = pickWaitngMemberPanel.transform.Find("Page0/TeamWithOneMember").GetComponent<TMP_Text>();
            oneMemberText.text = "팀원이 1명인 팀장은 ";
            for (int i = 0; i < teamsWithOneMember.Count; i++)
            {
                oneMemberText.text += teamsWithOneMember[i].leader[0].name;
                if (i < teamsWithOneMember.Count - 1)
                {
                    oneMemberText.text += ", ";
                }
            }
            oneMemberText.text += " 입니다.";
        }
        else
        {
            TMP_Text oneMemberText = pickWaitngMemberPanel.transform.Find("Page0/TeamWithOneMember").GetComponent<TMP_Text>();
            oneMemberText.text = "팀원이 1명인 팀장은 없습니다.";
        }
    }

    //대기 목록에 있는 팀원 선정 시작
    public void StartPickWaitingMember()
    {
        if(teamsWithZeroMembers.Count > 0)
        {
            //첫번째 teamWithZeroMembers의 팀장을 선택
            choiceManager.currentTeamIndex = teams.FirstOrDefault(t => t.leader[0].name == teamsWithZeroMembers[0].leader[0].name).leader[0].order;
            TMP_Text infoText = pickWaitngMemberPanel.transform.Find("Page1/InfoText").GetComponent<TMP_Text>();
            infoText.text = teamsWithZeroMembers[0].leader[0].name + "팀장님, 팀원을 선택해주세요.";
        }
        else if (teamsWithOneMember.Count > 0)
        {
            //첫번째 teamWithOneoMember의 팀장을 선택
            choiceManager.currentTeamIndex = teams.FirstOrDefault(t => t.leader[0].name == teamsWithOneMember[0].leader[0].name).leader[0].order;
            TMP_Text infoText = pickWaitngMemberPanel.transform.Find("Page1/InfoText").GetComponent<TMP_Text>();
            infoText.text = teamsWithOneMember[0].leader[0].name + "팀장님, 팀원을 선택해주세요.";
        }

        // WaitingMember_Choice라는 프리팹을 생성, Page1의 자식인 MemberGrid의 자식으로 생성, waitingTeamMembers의 수만큼 생성
        GameObject prefab = Resources.Load<GameObject>("Prefabs/WaitingMember_Choice");
        for (int i = 0; i < waitingTeamMembers.Count; i++)
        {
            if (waitingTeamMembers == null || waitingTeamMembers.Count == 0)
            {
                Debug.LogError("waitingTeamMembers 리스트가 비어 있습니다.");
                return;
            }

            if (i < 0 || i >= waitingTeamMembers.Count)
            {
                Debug.LogError($"잘못된 인덱스 접근: {i}");
                return;
            }

            int memberIndex = waitingTeamMembers[i].order;
            Debug.Log($"선택된 멤버의 인덱스: {memberIndex}");

            GameObject waitingMember = Instantiate(prefab, pickWaitngMemberPanel.transform.Find("Page1/MemberGrid"));
            Image image = waitingMember.transform.Find("Image").GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/TeamMember/" + waitingTeamMembers[i].imageName);
            TMP_Text name = waitingMember.transform.Find("Name").GetComponent<TMP_Text>();
            name.text = waitingTeamMembers[i].name;

            Button button = waitingMember.transform.Find("Button").GetComponent<Button>();
            button.onClick.AddListener(() => AddWaitingMemberToTeam(memberIndex));
        }
    }

    //대기 목록에 있는 팀원을 특정 팀에 추가

    public void AddWaitingMemberToTeam(int memberIndex)
    {
        // 선택당한 멤버가 현재 teamWithZeroMembers의 인덱스 차례에 해당하는 팀에 추가
        choiceManager.currentMemberIndex = memberIndex;
        Team team = teams.FirstOrDefault(t => t.leader[0].order == choiceManager.currentTeamIndex);
        TeamMember selectedMember = waitingTeamMembers.FirstOrDefault(m => m.order == choiceManager.currentMemberIndex);
        if (team != null && team.members.Count < MaxMembersPerTeam)
        {
            team.members.Add(selectedMember);
            UpdateTeamUI(choiceManager.currentTeamIndex, team.members.Count - 1, selectedMember);
            // 생성된 prefabs 중에서 selectedMember의 name과 동일한 TMP_Text를 가진 자식 오브젝트가 있다면 해당 prefabs 삭제
            Transform memberGrid = pickWaitngMemberPanel.transform.Find("Page1/MemberGrid");
            for (int i = 0; i < memberGrid.childCount; i++)
            {
                Transform child = memberGrid.GetChild(i);
                TMP_Text name = child.Find("Name").GetComponent<TMP_Text>();
                if (name.text == selectedMember.name)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
            //waitingTeamMembers에서 memberIndex와 같은 int의 order를 가진 멤버 삭제
            waitingTeamMembers.RemoveAll(m => m.order == memberIndex);
            CheckTeamMembersCount();
        }
    }

    //현재 choiceManger.currentTeamIndex에 해당하는 팀의 팀원이 2명이 넘는지 확인
    public void CheckTeamMembersCount()
    {
        Debug.Log("CheckTeamMembersCount 메서드가 호출되었습니다.");
        Team team = teams.FirstOrDefault(t => t.leader[0].order == choiceManager.currentTeamIndex);
        Debug.Log($"{team.leader[0].name}의 멤버 수: {team.members.Count}");
        if (teamsWithZeroMembers.Count > 0 && team.members.Count == MaxMembersPerTeam)
        {
            Debug.Log($"팀 {team.leader[0].name}이(가) 최대 멤버 수에 도달했습니다.");
            teamsWithZeroMembers.RemoveAt(0); 
        }
        else if (teamsWithOneMember.Count > 0 && team.members.Count == MaxMembersPerTeam)
        {
            Debug.Log($"팀 {team.leader[0].name}이(가) 최대 멤버 수에 도달했습니다.");
            teamsWithOneMember.RemoveAt(0);
        }
        NextPickWaitingMember();
    }

    public void NextPickWaitingMember()
    {
        if (teamsWithZeroMembers.Count > 0)
        {
            choiceManager.currentTeamIndex = teams.FirstOrDefault(t => t.leader[0].name == teamsWithZeroMembers[0].leader[0].name).leader[0].order;

            TMP_Text infoText = pickWaitngMemberPanel.transform.Find("Page1/InfoText").GetComponent<TMP_Text>();
            infoText.text = teamsWithZeroMembers[0].leader[0].name + "팀장님, 팀원을 선택해주세요.";
        }
        else if (teamsWithOneMember.Count > 0)
        {
            //첫번째 teamWithOneoMember의 팀장을 선택
            choiceManager.currentTeamIndex = teams.FirstOrDefault(t => t.leader[0].name == teamsWithOneMember[0].leader[0].name).leader[0].order;

            TMP_Text infoText = pickWaitngMemberPanel.transform.Find("Page1/InfoText").GetComponent<TMP_Text>();
            infoText.text = teamsWithOneMember[0].leader[0].name + "팀장님, 팀원을 선택해주세요.";
        }
        else
        {
            Debug.Log("모든 팀 매칭이 완료됐습니다.");
            pickWaitngMemberPanel.SetActive(false);
            showResultPanel.SetActive(true);
        }
    }
}