using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TeamManager;

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
    public TMP_Text kickText;
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
    public Image introduceImage;

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
            new TeamLeader("도린", 0),
            new TeamLeader("레아나", 1),
            new TeamLeader("루엘", 2),
            new TeamLeader("모모시로\n카나", 3),
            new TeamLeader("슨아", 4),
            new TeamLeader("아카샤", 5),
            new TeamLeader("채다빈", 6),
            new TeamLeader("타츠메\n유키", 7),
            new TeamLeader("토종\n아오리", 8),
            new TeamLeader("하얘", 9)
        };

        teamMembers = new List<TeamMember>
        {
        new TeamMember("그나로", 0, "그나로"),
        new TeamMember("노이", 1, "노이"),
        new TeamMember("뉴냥이", 2, "뉴냥이"),
        new TeamMember("달짱", 3, "달짱"),
        new TeamMember("디아나", 4, "디아나"),
        new TeamMember("루잼", 5, "루잼"),
        new TeamMember("리리스", 6, "리리스"),
        new TeamMember("미아", 7, "미아"),
        new TeamMember("뵤오", 8, "뵤오"),
        new TeamMember("소요카", 9, "소요카"),
        new TeamMember("옥쓔", 10, "옥쓔"),
        new TeamMember("이야", 11, "이야"),
        new TeamMember("쪼로퀸", 12, "쪼로퀸"),
        new TeamMember("카나프", 13, "카나프"),
        new TeamMember("카푸", 14, "카푸"),
        new TeamMember("코오리", 15, "코오리"),
        new TeamMember("코와가리\n리유", 16, "코와가리 리유"),
        new TeamMember("티 야", 17, "티 야"),
        new TeamMember("하야시\n이로", 18, "하야시 이로"),
        new TeamMember("헤스", 19, "헤스")
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
            //줄바꿈을 띄어쓰기로 변경
            string content = teamLeaders[i].name;
            string normalizedcontent = content.Replace("\n", " ");
            leaderImages[i].sprite = Resources.Load<Sprite>("Images/TeamLeader/" + normalizedcontent);
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
        introduceImage.sprite = Resources.Load<Sprite>("Images/IntroduceImage/" + choiceManager.currentTeamMember.imageName);
        Debug.Log(choiceManager.currentTeamMember.name);
        Debug.Log(choiceManager.currentTeamMember.order);
    }

    public void LoadNextMember(int index)
    {
        // 다음 멤버를 불러오고 초기화
        choiceManager.currentTeamMember = teamMembers[index];
        introduceImage.sprite = Resources.Load<Sprite>("Images/IntroduceImage/" + choiceManager.currentTeamMember.imageName);
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
        string teamLeaderContent = team.leader[0].name;
        string normalizedcontent = teamLeaderContent.Replace("\n", " ");
        kickText.text = $"{normalizedcontent} 팀장님\n내보낼 팀원을 선택해주세요";
        // kickMemberNames에 teamindex에 해당하는 팀원 이름과 choiceManager의 currentTeamMember 이름을 넣어줌
        string teamMember1Content = team.members[0].name;
        string teamMember2Content = team.members[1].name;
        string teamMember3Content = choiceManager.currentTeamMember.name;
        string normalized1content = teamMember1Content.Replace("\n", " ");
        string normalized2content = teamMember2Content.Replace("\n", " ");
        string normalized3content = teamMember3Content.Replace("\n", " ");
        kickMemberNames[0].text = normalized1content;
        kickMemberNames[1].text = normalized2content;
        kickMemberNames[2].text = normalized3content;
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
                string content = teamsWithZeroMembers[i].leader[0].name;
                string normalizedcontent = content.Replace("\n", " ");
                zeroMemberText.text += normalizedcontent;
                if (i < teamsWithZeroMembers.Count - 1)
                {
                    zeroMemberText.text += ", ";
                }
            }
            zeroMemberText.text += " 팀장 입니다.";

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
                string content = teamsWithOneMember[i].leader[0].name;
                string normalizedcontent = content.Replace("\n", " ");
                oneMemberText.text += normalizedcontent;
                if (i < teamsWithOneMember.Count - 1)
                {
                    oneMemberText.text += ", ";
                }
            }
            oneMemberText.text += " 팀장 입니다.";
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
            string content = teamsWithZeroMembers[0].leader[0].name;
            string normalizedcontent = content.Replace("\n", " ");
            infoText.text = normalizedcontent + " 팀장님, 팀원을 선택해주세요.";
        }
        else if (teamsWithOneMember.Count > 0)
        {
            //첫번째 teamWithOneoMember의 팀장을 선택
            choiceManager.currentTeamIndex = teams.FirstOrDefault(t => t.leader[0].name == teamsWithOneMember[0].leader[0].name).leader[0].order;
            TMP_Text infoText = pickWaitngMemberPanel.transform.Find("Page1/InfoText").GetComponent<TMP_Text>();
            string content = teamsWithOneMember[0].leader[0].name;
            string normalizedcontent = content.Replace("\n", " ");
            infoText.text = teamsWithOneMember[0].leader[0].name + " 팀장님, 팀원을 선택해주세요.";
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
            string content = teamsWithZeroMembers[0].leader[0].name;
            string normalizedcontent = content.Replace("\n", " ");
            infoText.text = normalizedcontent + " 팀장님, 팀원을 선택해주세요.";
        }
        else if (teamsWithOneMember.Count > 0)
        {
            //첫번째 teamWithOneoMember의 팀장을 선택
            choiceManager.currentTeamIndex = teams.FirstOrDefault(t => t.leader[0].name == teamsWithOneMember[0].leader[0].name).leader[0].order;

            TMP_Text infoText = pickWaitngMemberPanel.transform.Find("Page1/InfoText").GetComponent<TMP_Text>();
            string content = teamsWithOneMember[0].leader[0].name;
            string normalizedcontent = content.Replace("\n", " ");
            infoText.text = normalizedcontent + " 팀장님, 팀원을 선택해주세요.";
        }
        else
        {
            Debug.Log("모든 팀 매칭이 완료됐습니다.");
            pickWaitngMemberPanel.SetActive(false);
            showResultPanel.SetActive(true);
        }
    }
}